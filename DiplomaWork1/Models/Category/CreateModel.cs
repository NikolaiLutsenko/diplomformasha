using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models.Category
{
    public class CreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Введите имя категории")]
        [Display(Name = "Имя категории")]
        public string Name { get; set; }
    }
}
