using System;
using System.Collections.Generic;

namespace DiplomaWork.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Service> Services { get; set; }
    }
}
