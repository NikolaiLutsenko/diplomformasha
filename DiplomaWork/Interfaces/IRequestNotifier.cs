using DiplomaWork.Data.Models;
using System;
using System.Threading.Tasks;

namespace DiplomaWork.Interfaces
{
    public interface IRequestNotifier
    {
        Task NotifyAsync(Guid requestId, RequestStateType state);
    }
}
