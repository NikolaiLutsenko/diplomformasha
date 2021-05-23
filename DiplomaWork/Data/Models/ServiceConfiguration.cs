using DiplomaWork1.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DiplomaWork1.Data.Models
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        private static Guid AnotherProblemPhoneId = new Guid("60b56aac-db99-4ebe-b828-2b4ba40ae15b");
        private static Guid AnotherProblemPrintersId = new Guid("3c88e7cd-8756-4aa1-a098-787caefcb3a5");
        private static Guid AnotherProblemNoteBooksId = new Guid("be42a735-12c9-4237-9637-ec4bd3073d98");

        private static Guid ChangeDisplayPhonesId = new Guid("66d89e49-d6be-4120-85f2-a1a21736c709");
        private static Guid ChangeAkkumPhonesId = new Guid("2e775478-eb40-4fea-aafa-261e6f6db1fb");

        private static Guid ChangeCartredgePrintersId = new Guid("6edeeec0-e147-47d4-9bbe-3db9938b0d75");

        private static Guid ChangeKeyboardNoteBookId = new Guid("b11d6469-2a3b-4b8d-86ff-a5a9861e9384");
        private static Guid ChangeMatrixNoteBookId = new Guid("52347af0-0366-441b-b881-268924d4fa04");
        private static Guid CleanupNoteBookId = new Guid("21c474f2-47e1-41c5-b755-c2e47c4694ca");




        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.CategoryId).IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.Name, x.CategoryId }).IsUnique();

            builder.HasData(new Service[]
            {
                new Service{ Id = AnotherProblemPhoneId, CategoryId = DefaultCategories.PhonesCategoryId, Name = "Другое" },
                new Service{ Id = AnotherProblemPrintersId, CategoryId = DefaultCategories.PrintersCategoryId, Name = "Другое" },
                new Service{ Id = AnotherProblemNoteBooksId, CategoryId = DefaultCategories.NoteBooksCategoryId, Name = "Другое" },
                new Service{ Id = ChangeDisplayPhonesId, CategoryId = DefaultCategories.PhonesCategoryId, Name = "Замена экрана" },
                new Service{ Id = ChangeAkkumPhonesId, CategoryId = DefaultCategories.PhonesCategoryId, Name = "Замена аккумулятора" },
                new Service{ Id = ChangeCartredgePrintersId, CategoryId = DefaultCategories.PrintersCategoryId, Name = "Заправка картреджа" },
                new Service{ Id = ChangeKeyboardNoteBookId, CategoryId = DefaultCategories.NoteBooksCategoryId, Name = "Проблема с клавиатурой" },
                new Service{ Id = ChangeMatrixNoteBookId, CategoryId = DefaultCategories.NoteBooksCategoryId, Name = "Замена матрицы" },
                new Service{ Id = CleanupNoteBookId, CategoryId = DefaultCategories.NoteBooksCategoryId, Name = "Чистка компьютера" },
            });
        }
    }
}
