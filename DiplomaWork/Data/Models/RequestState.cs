using System;

namespace DiplomaWork.Data.Models
{
    public class RequestState
    {
        public Guid Id { get; set; }

        public Guid RequestId { get; set; }

        public Guid UserId { get; set; }

        public string State { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }

        public Request Request { get; set; }
    }
}
