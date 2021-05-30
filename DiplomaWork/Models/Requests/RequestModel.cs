using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Requests
{
    public class RequestModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Услуга")]
        public string ServiceName { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Почта клиента")]
        public string UserEmail { get; set; }

        [Display(Name = "Почта тех специалиста")]
        public string EmployeeEmail { get; set; }

        public ShowBadgeModel Badget { get; set; }

        public IEnumerable<RequestStateModel> States { get; set; }
        public Guid? CurrentEmployeeId { get; set; }
    }
}
