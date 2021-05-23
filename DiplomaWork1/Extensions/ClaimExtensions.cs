using DiplomaWork1.Models.Constants;
using System;
using System.Security.Claims;

namespace DiplomaWork1.Extensions
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
