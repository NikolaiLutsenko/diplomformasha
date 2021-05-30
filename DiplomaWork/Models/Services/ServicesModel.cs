using Microsoft.AspNetCore.Mvc;
using System;

namespace DiplomaWork.Models.Services
{
    public class ServicesModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid? CategoryId { get; set; }

        public ServiceModel[] Services { get; set; }
    }
}
