using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaWork1.Data;
using DiplomaWork1.Data.Models;
using DiplomaWork1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace DiplomaWork1
{
    // hi Masha

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStr = Configuration.GetConnectionString("Default");
            services.AddDbContext<DiplomaWorkContext>(dbBuilder =>
            {
                dbBuilder.UseMySql(connectionStr, options => options.ServerVersion(new Version(8, 0), ServerType.MySql));
            });
            services.AddIdentity<User, Role>(opts => {
                opts.Password.RequiredLength = 5;   // ����������� �����
                opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                opts.Password.RequireDigit = false; // ��������� �� �����

                opts.User.AllowedUserNameCharacters = null;
            }).AddEntityFrameworkStores<DiplomaWorkContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews();

            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}
