using DiplomaWork.Models.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaWork.Data.Models
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasData(new Role[]
            {
                new Role{ Id = RoleConstants.AdminId, Name = RoleConstants.Admin, NormalizedName = RoleConstants.Admin.ToUpper(), ConcurrencyStamp = "a5336036-976d-4611-ad8d-27bf11d3c717" },
                new Role{ Id = RoleConstants.HrManagerId, Name = RoleConstants.HrManager, NormalizedName = RoleConstants.HrManager.ToUpper(), ConcurrencyStamp = "bbdc4c3b-1a7e-4a64-8e85-47f1eb94e0be" },
                new Role{ Id = RoleConstants.TechnicalSpecialistId, Name = RoleConstants.TechnicalSpecialist, NormalizedName = RoleConstants.TechnicalSpecialist.ToUpper(), ConcurrencyStamp = "5dfc555b-6bfe-442c-9846-1c3c2ca7d582" },
                new Role{ Id = RoleConstants.QualityControlId, Name = RoleConstants.QualityControl, NormalizedName = RoleConstants.QualityControl.ToUpper(), ConcurrencyStamp = "a8d1f88f-99b7-4ed4-82cb-f6cc5e91b472" }
            });
        }
    }
}
