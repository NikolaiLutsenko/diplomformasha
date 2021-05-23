using DiplomaWork1.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaWork1.Data.Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany<Service>(x => x.Services)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Category[]
            {
                new Category { Id = DefaultCategories.PhonesCategoryId, Name = "Телефоны"},
                new Category { Id = DefaultCategories.NoteBooksCategoryId, Name = "Ноутбуки"},
                new Category { Id = DefaultCategories.PrintersCategoryId, Name = "Принтеры"},
            });
        }
    }
}
