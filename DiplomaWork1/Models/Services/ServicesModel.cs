using Microsoft.AspNetCore.Mvc;
using System;

namespace DiplomaWork1.Models.Services
{
    public class ServicesModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid? CategoryId { get; set; }

        public ServiceModel[] Services { get; set; }
    }
}
