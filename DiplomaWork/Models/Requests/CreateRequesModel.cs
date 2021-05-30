using DiplomaWork.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Requests
{
    public class CreateRequesModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserName { get; set; }

        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserEmail { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserPhone { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Услуга")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Guid ServiceId { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Guid CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<CategoryModel> Services { get; set; }
    }
}
