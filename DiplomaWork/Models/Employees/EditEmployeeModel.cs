using DiplomaWork1.Data.Models;
using DiplomaWork1.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models.Employees
{
    public class EditEmployeeModel
    {

        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "ФИО")]
        [Required]
        public string UserName { get; set; }

        public IEnumerable<Service> ExistingServices { get; set; }

        public Guid[] UserServiceIds { get; set; }
    }
}
