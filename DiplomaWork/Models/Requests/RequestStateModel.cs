using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Requests
{
    public class RequestStateModel
    {
        [Display(Name = "Почта тех специалиста")]
        public string EmployeeEmail { get; set; }

        [Display(Name = "Дата статуса")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Название")]
        public string State { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
