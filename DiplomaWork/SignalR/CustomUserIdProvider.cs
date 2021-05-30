using DiplomaWork.Models.Constants;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace DiplomaWork.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims.FirstOrDefault(x => x.Type == ClaimConstants.EmployeeId)?.Value;
        }
    }
}
