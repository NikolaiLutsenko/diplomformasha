namespace DiplomaWork1.Models.Requests
{
    public class MyRequestsModel
    {
        public RequestModel[] CompletedRequests { get; set; }

        public RequestModel[] WorkInProgressRequests { get; set; }

        public RequestModel[] NotAssignedRequests { get; set; }
    }
}
