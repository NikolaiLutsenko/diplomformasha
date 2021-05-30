using DiplomaWork.Data;
using DiplomaWork.Interfaces;
using DiplomaWork.Models.Admin;
using DiplomaWork.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class AdminController : Controller
    {
        private readonly DiplomaWorkContext _db;
        private readonly IRequestService _requestService;

        public AdminController(DiplomaWorkContext db, IRequestService requestService)
        {
            _db = db;
            _requestService = requestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Requests()
        {
            var requests = await _requestService.GetRequestsAsync();

            return View(new AdminRequestsModel
            {
                CompletedRequests = requests.Where(x => x.Badget.IsCompleted && x.CurrentEmployeeId.HasValue).ToArray(),
                WorkInProgressRequests = requests.Where(x => !x.Badget.IsCompleted && !x.Badget.IsReturned && x.CurrentEmployeeId.HasValue).ToArray(),
                NotAssignedRequests = requests.Where(x => !x.CurrentEmployeeId.HasValue).ToArray(),
                ReturnedRequests = requests.Where(x => x.Badget.IsReturned).ToArray()
            });
        }
    }
}
