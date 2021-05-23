using DiplomaWork1.Extensions;
using DiplomaWork1.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DiplomaWork1.Controllers
{
    public abstract class BaseController: Controller
    {
        protected bool IsCurrentUserAdmin() => User != null && User.IsInRole(RoleConstants.Admin);

        protected bool IsCurrentUserQualityControl() => User != null && User.IsInRole(RoleConstants.QualityControl);

        protected bool IsCurrentUserTechnicalSpecialist() => User != null && User.IsInRole(RoleConstants.TechnicalSpecialist);

        protected bool IsCurrentUser(Guid userId) => User != null && User.GetEmployeeId() == userId;

        protected RedirectToActionResult RedirectToNotFound(string? message = null)
        {
            return RedirectToAction(nameof(ErrorController.NotFound), "Error", new { message = message });
        }

        protected RedirectToActionResult RedirectToNotAllowed(string? message = null)
        {
            return RedirectToAction(nameof(ErrorController.NotAllowed), "Error", new { message = message });
        }
    }
}
