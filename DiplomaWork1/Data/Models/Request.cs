using System;
using System.Collections.Generic;

namespace DiplomaWork1.Data.Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string UserPhone { get; set; }

        public string Description { get; set; }

        public Guid? CurrentEmployeeId { get; set; }

        public User CurrentEmployee { get; set; }

        public Service Service { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsReturned { get; set; }

        public bool IsWaitingQualityControl { get; set; }

        public IEnumerable<RequestState> States { get; set; }
    }
}
