using DiplomaWork.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DiplomaWork.Data
{
    public class DiplomaWorkContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<UserService> UserServices { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestState> RequestStates { get; set; }

        public DiplomaWorkContext(DbContextOptions<DiplomaWorkContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new RoleConfiguration())
                .ApplyConfiguration(new CategoryConfiguration())
                .ApplyConfiguration(new ServiceConfiguration())
                .ApplyConfiguration(new UserServiceConfiguration())
                .ApplyConfiguration(new RequestConfiguration())
                .ApplyConfiguration(new RequestStateConfiguration());
        }
    }
}
