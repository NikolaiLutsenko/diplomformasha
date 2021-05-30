using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models.Requests
{
    public class AssignEmployeeModel
    {
        public IEnumerable<SelectListItem> Employees { get; set; }

        [Display(Name = "Исполнитель")]
        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid? EmployeeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid RequestId { get; set; }
    }
}
