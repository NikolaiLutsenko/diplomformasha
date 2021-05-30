using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaWork.Data;
using DiplomaWork.Data.Models;
using DiplomaWork.Extensions;
using DiplomaWork.Interfaces;
using DiplomaWork.Models.Category;
using DiplomaWork.Models.Constants;
using DiplomaWork.Models.Requests;
using DiplomaWork.Models.Services;
using DiplomaWork.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DiplomaWork.Controllers
{
    [AllowAnonymous]
    public class RequestsController : BaseController
    {
        private readonly DiplomaWorkContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IRequestService _requestService;

        public RequestsController(DiplomaWorkContext db, UserManager<User> userManager, IRequestService requestService)
        {
            _db = db;
            _userManager = userManager;
            _requestService = requestService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.TechnicalSpecialist)]
        public async Task<IActionResult> Index(Guid? employeeId)
        {
            if (!employeeId.HasValue && !IsCurrentUserAdmin())
            {
                ModelState.AddModelError(string.Empty, "Ошибка получения заявок");
                return RedirectToAction("Index", "Home");
            }

            var requests = await _requestService.GetRequestsAsync(employeeId, RequestsFlag.Unnassigned | RequestsFlag.IsNotWaitingQualityControl);

            return View(new MyRequestsModel
            {
                CompletedRequests = requests.Where(x => x.Badget.IsCompleted && x.CurrentEmployeeId == employeeId.Value).ToArray(),
                WorkInProgressRequests = requests.Where(x => !x.Badget.IsCompleted && x.CurrentEmployeeId == employeeId.Value).ToArray(),
                NotAssignedRequests = requests.Where(x => !x.CurrentEmployeeId.HasValue).ToArray()
            });

            
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.TechnicalSpecialist + "," + RoleConstants.QualityControl)]
        public async Task<IActionResult> Details(Guid id)
        {
            var request = await _db.Requests
                .Include(x => x.CurrentEmployee)
                .Include(x => x.States)
                .ThenInclude(state => state.User)
                .Include(x => x.Service)
                .ThenInclude(service => service.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return RedirectToNotFound("Заявка не найдена");
            }

            var userServiceIds = await _db.UserServices
                .Where(x => x.UserId == User.GetEmployeeId())
                .Select(x => x.ServiceId)
                .ToArrayAsync();

            var hasViewAccess = IsCurrentUserAdmin() || IsCurrentUserQualityControl();
            if (!hasViewAccess && !IsCurrentUser(request.CurrentEmployeeId.Value))
            {
                return RedirectToNotAllowed("Вы не можете смотреть эту заявку");
            }

            return View(new DetailsModel
            {
                RequestId = request.Id,
                CreatedDate = request.CreatedDate,
                EmployeeEmail = request.CurrentEmployee?.Email,
                EmployeeId = request.CurrentEmployeeId,
                Description = request.Description,
                ServiceName = request.Service.Name,
                CategoryName = request.Service.Category.Name,
                UserEmail = request.UserEmail,
                UserName = request.UserName,
                UserPhone = request.UserPhone,
                Badget = new ShowBadgeModel(request.StateType),
                CanAssignToCurrentEmployee = IsCurrentUserTechnicalSpecialist() && userServiceIds.Contains(request.ServiceId) && request.CurrentEmployeeId != User.GetEmployeeId(),
                States = request.States
                    .OrderByDescending(x => x.CreatedDate)
                    .Select(x => new RequestStateModel
                    {
                        State = x.State,
                        Description = x.Description,
                        CreatedDate = x.CreatedDate,
                        EmployeeEmail = x.User.Email,
                    })
            });
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AssignEmployee(Guid requestId)
        {
            var request = await _db.Requests
                .Include(x => x.CurrentEmployee)
                .FirstOrDefaultAsync(x => x.Id == requestId);

            if (request == null)
            {
                return RedirectToNotFound("Заявка не найдена");
            }

            var employees = await _db.UserServices
                .Include(x => x.User)
                .Where(x => x.ServiceId == request.ServiceId)
                .ToArrayAsync();

            return View(new AssignEmployeeModel
            {
                RequestId = requestId,
                Employees = employees.Select(x => new SelectListItem(x.User.Email, x.UserId.ToString("N")))
            });
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.TechnicalSpecialist)]
        public async Task<IActionResult> AssignEmployee([FromForm] AssignEmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Ошибка сохранения");
                return View(model);
            }

            var request = await _db.Requests.FirstOrDefaultAsync(x => x.Id == model.RequestId);
            if (request == null)
            {
                return RedirectToNotFound("Заявка не найдена");
            }
            var user = await _db.UserServices
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == model.EmployeeId && x.ServiceId == request.ServiceId);

            if (user == null)
            {
                return RedirectToNotFound("Исполнитель не найден");
            }

            request.CurrentEmployeeId = user.UserId;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = model.RequestId.ToString("N") });
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.TechnicalSpecialist + "," + RoleConstants.QualityControl)]
        public async Task<IActionResult> SetState(Guid id)
        {
            var request = await _db.Requests.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return RedirectToNotFound("Заявка не найдена");
            }

            if (request.CurrentEmployeeId != User.GetEmployeeId())
            {
                return RedirectToNotAllowed("Вы не можете устанавливать статусы");
            }

            return View(new SetStateModel
            {
                RequestId = id,
                IsOnQuolityControl = request.StateType == RequestStateType.WaitingQualityControl
            });
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.TechnicalSpecialist + "," + RoleConstants.QualityControl)]
        public async Task<IActionResult> SetState([FromForm] SetStateModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Ошибка сохранения");
                return View(model);
            }

            var request = await _db.Requests.FindAsync(model.RequestId);
            if (request == null)
            {
                ModelState.AddModelError(string.Empty, "Заявка не найдена");
                return View(model);
            }

            if (request.CurrentEmployeeId != User.GetEmployeeId())
            {
                ModelState.AddModelError(string.Empty, "Вы не можете устанавливать статусы");
                return View(model);
            }

            await _requestService.SetStateAsync(model, User.GetEmployeeId());

            return RedirectToAction(nameof(Details), new { id = model.RequestId });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _db.Categories.ToListAsync();
            return View(new CreateRequesModel
            {
                Services = Enumerable.Empty<CategoryModel>().ToList(),
                Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRequesModel requesModel)
        {
            if (ModelState.IsValid)
            {
                if (!await _requestService.TryCreateRequestAsync(requesModel))
                    return RedirectToAction(nameof(Create), requesModel);
                return RedirectToAction("Index", "Home", new { message = "Заявка создана" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ошибка создания заявки");
                var categories = await _db.Categories.ToListAsync();
                requesModel.Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
                return View(requesModel);
            }
        }

        public async Task<ActionResult> GetServices(Guid id)
        {
            var services = await _db.Services.Where(x => x.CategoryId == id).ToListAsync();
            return PartialView(services.Select(x => new ServiceModel { Id = x.Id, Name = x.Name }));
        }
    }
}
