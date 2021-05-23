using System;
using System.Collections.Generic;

namespace DiplomaWork1.Data.Models
{
    public class Service
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public IEnumerable<UserService> UserServices { get; set; }

        public IEnumerable<Request> Requests { get; set; }
    }
}
