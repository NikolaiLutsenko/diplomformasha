using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models.Requests
{
    public class SetStateModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid RequestId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsOnQuolityControl { get; set; }

        [Required(ErrorMessage = "Поле статус обязательно для ввода")]
        [Display(Name = "Статус")]
        public string State { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "На проверку качества")]
        public bool ToQualityControl { get; set; }

        [Display(Name = "Вернуть тех специалисту")]
        public bool ToTechnicalSpecialist { get; set; }

        [Display(Name = "Завершить")]
        public bool IsCompleted { get; set; }
    }
}
