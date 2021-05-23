﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models.Roles
{
    public class EditEmployeeRolesModel
    {
        [Display(Name = "ФИО")]
        public string UserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid UserId { get; set; }

        [Display(Name = "Роли")]
        public RoleModel[] AllRoles { get; set; }

        public string[] UserRoles { get; set; }
    }
}
