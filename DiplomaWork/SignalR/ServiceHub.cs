using DiplomaWork.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace DiplomaWork.SignalR
{
    public class ServiceHub: Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly DiplomaWorkContext _db;

        public ServiceHub(DiplomaWorkContext db, UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
    }
}
