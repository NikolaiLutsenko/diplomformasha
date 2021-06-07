using DiplomaWork.Data.Models;
using DiplomaWork.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Employees
{
    public class EditEmployeeModel
    {

        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string UserName { get; set; }

        public IEnumerable<Service> ExistingServices { get; set; }

        public Guid[] UserServiceIds { get; set; }
    }
}
