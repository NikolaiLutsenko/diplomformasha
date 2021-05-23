using DiplomaWork1.Models.Requests;

namespace DiplomaWork1.Models.Admin
{
    public class AdminRequestsModel
    {
        public RequestModel[] CompletedRequests { get; set; }
        public RequestModel[] WorkInProgressRequests { get; set; }
        public RequestModel[] NotAssignedRequests { get; set; }
        public RequestModel[] ReturnedRequests { get; set; }
    }
}
