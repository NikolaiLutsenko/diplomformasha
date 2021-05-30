using DiplomaWork.Models.Constants;
using System;
using System.Security.Claims;

namespace DiplomaWork.Extensions
{
    public static class ClaimExtensions
    {
        public static Guid GetEmployeeId(this ClaimsPrincipal target)
        {
            var userId = target.FindFirstValue(ClaimConstants.EmployeeId);
            return string.IsNullOrEmpty(userId) ? Guid.Empty : new Guid(userId);
        }
    }
}
