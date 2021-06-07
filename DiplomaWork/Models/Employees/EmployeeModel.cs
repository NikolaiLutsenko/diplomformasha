using DiplomaWork.Data.Models;
using DiplomaWork.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork.Models.Employees
{
    public class EmployeeModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name = "Услуги")]
        public IEnumerable<ServiceModel> Services { get; set; }

        [Display(Name = "Роли")]
        public IEnumerable<string> Roles { get; set; }

        [Display(Name = "Количество заявок")]
        public int CountOfRequests { get; set; }
    }
}
