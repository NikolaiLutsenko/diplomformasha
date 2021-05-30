using DiplomaWork.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestModel>> GetRequestsAsync(Guid? employeeId = null, RequestsFlag flags = RequestsFlag.None);

        Task SetStateAsync(SetStateModel model, Guid employeeId);

        Task<bool> TryCreateRequestAsync(CreateRequesModel model);
    }
}
