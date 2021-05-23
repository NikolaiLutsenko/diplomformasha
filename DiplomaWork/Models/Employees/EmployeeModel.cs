using DiplomaWork1.Data.Models;
using DiplomaWork1.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork1.Models.Employees
{
    public class EmployeeModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "ФИО")]
        public string UserName { get; set; }

        [Display(Name = "Услуги")]
        public IEnumerable<ServiceModel> Services { get; set; }

        [Display(Name = "Роли")]
        public IEnumerable<string> Roles { get; set; }

        [Display(Name = "Количество заявок")]
        public int CountOfRequests { get; set; }
    }
}
