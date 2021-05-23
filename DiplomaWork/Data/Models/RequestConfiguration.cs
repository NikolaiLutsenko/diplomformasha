using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaWork1.Data.Models
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("requests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.ServiceId).IsRequired();

            builder.Property(x => x.UserEmail).IsRequired();

            builder.Property(x => x.UserName).IsRequired();

            builder.Property(x => x.UserPhone).IsRequired();

            builder.Property(x => x.CurrentEmployeeId);

            builder.Property(x => x.Description);

            builder.Property(x => x.CreatedDate);

            builder.Property(x => x.IsCompleted).IsRequired();

            builder.Property(x => x.IsReturned).IsRequired();

            builder.Property(x => x.IsWaitingQualityControl).IsRequired();

            builder.HasOne(x => x.CurrentEmployee)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.CurrentEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
