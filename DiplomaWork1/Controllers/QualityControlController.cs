using DiplomaWork1.Data;
using DiplomaWork1.Models.Constants;
using DiplomaWork1.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork1.Controllers
{
    [Authorize(Roles = RoleConstants.QualityControl + "," + RoleConstants.Admin)]
    public class QualityControlController: BaseController
    {
        private readonly DiplomaWorkContext _db;

        public QualityControlController(DiplomaWorkContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _db.Requests
                .Include(x => x.Service)
                .ThenInclude(service => service.Category)
                .Include(x => x.States)
                .Include(x => x.CurrentEmployee)
                .Where(x => x.IsWaitingQualityControl)
                .ToArrayAsync();

            return View(requests.Select(x => new RequestModel
            {
                Id = x.Id,
                ServiceName = x.Service.Name,
                CategoryName = x.Service.Category.Name,
                CreatedAt = x.CreatedDate,
                UserEmail = x.UserEmail,
                EmployeeEmail = x.CurrentEmployee?.Email,
                Badget = new ShowBadgeModel(x.IsCompleted, x.IsReturned, x.IsWaitingQualityControl),
                States = x.States
                    .OrderByDescending(x => x.CreatedDate)
                    .Select(s => new RequestStateModel
                    {
                        CreatedDate = s.CreatedDate,
                        State = s.State,
                        Description = s.Description,
                        EmployeeEmail = s.User.Email
                    })
            }));
        }
    }
}
