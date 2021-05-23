using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models.Requests
{
    public class DetailsModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid RequestId { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Почта сотрудника")]
        public string EmployeeEmail { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? EmployeeId { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Услуга")]
        public string ServiceName { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        [Display(Name = "Почта клиента")]
        public string UserEmail { get; set; }

        [Display(Name = "Имя клиента")]
        public string UserName { get; set; }

        [Display(Name = "Телефон клиента")]
        public string UserPhone { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool CanAssignToCurrentEmployee { get; set; }

        public ShowBadgeModel Badget { get; set; }

        public IEnumerable<RequestStateModel> States { get; set; }

    }
}
