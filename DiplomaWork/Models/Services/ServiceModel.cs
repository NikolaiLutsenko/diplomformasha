using DiplomaWork.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Services
{
    public class ServiceModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid? Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Поле категория обязательно для ввода")]
        public Guid? CategoryId { get; set; }

        [Display(Name = "Услуга")]
        [Required(ErrorMessage = "Поле услуга обязательно для ввода")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public CategoryModel Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
