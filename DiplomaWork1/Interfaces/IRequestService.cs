using DiplomaWork1.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWork1.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestModel>> GetRequestsAsync(Guid? employeeId = null, RequestsFlag flags = RequestsFlag.None);
    }
}
