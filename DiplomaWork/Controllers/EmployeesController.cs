using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaWork.Data;
using DiplomaWork.Data.Models;
using DiplomaWork.Models.Category;
using DiplomaWork.Models.Constants;
using DiplomaWork.Models.Employees;
using DiplomaWork.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiplomaWork.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class EmployeesController : Controller
    {
        private readonly DiplomaWorkContext _db;
        private readonly UserManager<User> _userManager;

        public EmployeesController(DiplomaWorkContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users
                .Include(x => x.Requests)
                .Include(x => x.UserServices)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.Category).ToList();
            var result = new List<EmployeeModel>(users.Count);
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new EmployeeModel
                {
                    Id = user.Id,
                    Email = user.Email.ToLower(),
                    UserName = user.UserName,
                    Services = user.UserServices.Select(us => new ServiceModel
                    {
                        Id = us.ServiceId,
                        CategoryId = us.Service.CategoryId,
                        Name = us.Service.Name,
                        Category = new CategoryModel
                        {
                            Id = us.Service.CategoryId,
                            Name = us.Service.Category.Name
                        }
                    }),
                    Roles = roles,
                    CountOfRequests = user.Requests.Count()
                });
            }

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = _userManager.Users
                .Include(x => x.UserServices)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var services = await _db.Services.Include(x => x.Category).ToListAsync();
            return View(new EditEmployeeModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ExistingServices = services,
                UserServiceIds = user.UserServices.Select(x => x.ServiceId).ToArray()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditEmployeeModel employeeModel)
        {
            var user = _userManager.Users
                .Include(x => x.UserServices)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == employeeModel.Id);

            var userServicesToRemove = user.UserServices.Where(us => !employeeModel.UserServiceIds.Contains(us.ServiceId));
            _db.RemoveRange(userServicesToRemove);
            var servicesToAdd = employeeModel.UserServiceIds.Except(user.UserServices.Select(x => x.ServiceId).ToList());
            _db.UserServices.AddRange(servicesToAdd.Select(x => new UserService { UserId = user.Id, ServiceId = x }));
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id.ToString());
            if (userToDelete != null)
            {
                var userServices = await _db.UserServices.Where(x => x.UserId == id).ToArrayAsync();
                _db.UserServices.RemoveRange(userServices);

                var roles = await _userManager.GetRolesAsync(userToDelete);
                if (roles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(userToDelete, roles);
                }

                var requests = await _db.Requests.Where(x => x.CurrentEmployeeId == id).ToArrayAsync();
                if (requests.Count() > 0)
                {
                    foreach (var request in requests)
                    {
                        request.CurrentEmployeeId = null;
                    }
                }

                await _db.SaveChangesAsync();

                await _userManager.DeleteAsync(userToDelete);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
