using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaWork.Data.Models
{
    public class RequestStateConfiguration : IEntityTypeConfiguration<RequestState>
    {
        public void Configure(EntityTypeBuilder<RequestState> builder)
        {
            builder.ToTable("requeststates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.RequestId).IsRequired();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.State).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(250);

            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.RequestStates)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Request)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
