using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Category
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Категория")]
        public string Name { get; set; }
    }
}
