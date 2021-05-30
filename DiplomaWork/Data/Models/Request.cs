using System;
using System.Collections.Generic;

namespace DiplomaWork.Data.Models
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

        public RequestStateType StateType { get; set; }

        public IEnumerable<RequestState> States { get; set; }
    }
}
