using DiplomaWork1.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DiplomaWork1.Data
{
    public class User: IdentityUser<Guid>
    {
        public IEnumerable<UserService> UserServices { get; set; }

        public IEnumerable<Request> Requests { get; set; }

        public IEnumerable<RequestState> RequestStates { get; set; }
    }
}
