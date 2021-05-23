using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using DiplomaWork1.Models.Constants;
using DiplomaWork1.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork1.Controllers
{
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.HrManager)]
    public class RolesController : Controller
    {
        private readonly DiplomaWorkContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RolesController(
            DiplomaWorkContext db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployeeRoles(Guid id)
        {
            var roles = _roleManager.Roles.ToList();
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(EmployeesController.Index), "Employees");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new EditEmployeeRolesModel
            {
                UserId = user.Id,
                UserRoles = userRoles.ToArray(),
                AllRoles = roles.Select(x => new RoleModel { Id = x.Id, Name = x.Name }).ToArray(),
                UserName = user.UserName
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeRoles([FromForm] EditEmployeeRolesModel rolesModel)
        {
            var user = await _db.Users.FindAsync(rolesModel.UserId);
            if (user == null)
            {
                return RedirectToAction(nameof(EmployeesController.Index), "Employees");
            }
            var oldUserRoles = await _userManager.GetRolesAsync(user);
            var rolesToDelete = oldUserRoles.Except(rolesModel.UserRoles);
            var rolesToAdd = rolesModel.UserRoles.Except(oldUserRoles);

            await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
            await _userManager.AddToRolesAsync(user, rolesToAdd);

            return RedirectToAction(nameof(EmployeesController.Index), "Employees");
        }
    }
}
