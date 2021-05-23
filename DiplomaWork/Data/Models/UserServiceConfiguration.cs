using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaWork1.Data.Models
{
    public class UserServiceConfiguration : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder.ToTable("userservices");

            builder.HasKey(x => new { x.UserId, x.ServiceId });

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.ServiceId).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserServices)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.UserServices)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
