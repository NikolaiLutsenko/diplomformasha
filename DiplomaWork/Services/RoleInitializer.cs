using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DiplomaWork1.Services
{
    public static class RoleInitializer
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "Admin12345!";
        private const string AdminRole = "Admin";

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var user = await userManager.FindByEmailAsync(AdminEmail);
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    NormalizedEmail = AdminEmail,
                    UserName = "Админ"
                };
                var result = await userManager.CreateAsync(user, AdminPassword);
            }

            if (!await userManager.IsInRoleAsync(user, AdminRole))
            {
                await userManager.AddToRoleAsync(user, AdminRole);
            }
        }
    }
}
