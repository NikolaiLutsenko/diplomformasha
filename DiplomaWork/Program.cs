using System;
using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using DiplomaWork1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiplomaWork1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                RoleInitializer.InitializeAsync(userManager, roleManager).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
