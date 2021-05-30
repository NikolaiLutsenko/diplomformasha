using System;

namespace DiplomaWork.Data.Models
{
    public class UserService
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public User User { get; set; }

        public Service Service { get; set; }
    }
}
