using DiplomaWork.Data;
using DiplomaWork.Data.Models;
using DiplomaWork.Interfaces;
using DiplomaWork.Models.Constants;
using DiplomaWork.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DiplomaWork.Services
{
    public class RequestNotifier : IRequestNotifier
    {
        private readonly IHubContext<ServiceHub> _hubContext;
        private readonly DiplomaWorkContext _db;

        #region Events

        private const string RequestStateChanged = nameof(RequestStateChanged);

        #endregion

        public RequestNotifier(IHubContext<ServiceHub> hubContext, DiplomaWorkContext db)
        {
            _hubContext = hubContext;
            _db = db;
        }

        public async Task NotifyAsync(Guid requestId, RequestStateType state)
        {
            var request = await _db.Requests
                    .Include(x => x.CurrentEmployee)
                    .FirstOrDefaultAsync(x => x.Id == requestId);

            if ((state == RequestStateType.InProgress || state == RequestStateType.Returned) && request.CurrentEmployeeId.HasValue)
            {
                var message = state == RequestStateType.InProgress
                    ? "Создана новая заявка"
                    : "Заявка возвращена на доработку!";

                await _hubContext.Clients.User(request.CurrentEmployeeId.ToString()).SendAsync(RequestStateChanged, requestId, message);
            }
            if (state == RequestStateType.WaitingQualityControl)
            {
                await _hubContext.Clients.Group(SignalRGroupConstants.QuolityControlGroup).SendAsync(RequestStateChanged, requestId, $"Новая заявка для проверки контроля качества!");
            }
        }
    }
}
