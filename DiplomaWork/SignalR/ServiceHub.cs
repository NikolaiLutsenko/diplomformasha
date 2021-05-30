using DiplomaWork.Models.Constants;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DiplomaWork.SignalR
{
    public class ServiceHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (!string.IsNullOrEmpty(Context.UserIdentifier) && Context.User.IsInRole(RoleConstants.QualityControl))
            {
                await this.Groups.AddToGroupAsync(Context.ConnectionId, SignalRGroupConstants.QuolityControlGroup);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (!string.IsNullOrEmpty(Context.UserIdentifier) && Context.User.IsInRole(RoleConstants.QualityControl))
            {
                await this.Groups.RemoveFromGroupAsync(Context.ConnectionId, SignalRGroupConstants.QuolityControlGroup);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
