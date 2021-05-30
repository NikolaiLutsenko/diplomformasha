using DiplomaWork.Data.Models;

namespace DiplomaWork.Models.Requests
{
    public class ShowBadgeModel
    {

        public ShowBadgeModel(RequestStateType requestStateType)
        {
            IsCompleted = requestStateType == RequestStateType.Completed;
            IsReturned = requestStateType == RequestStateType.Returned;
            IsWaitingQualityControl = requestStateType == RequestStateType.WaitingQualityControl;
        }

        public bool IsCompleted { get; }

        public bool IsReturned { get; }

        public bool IsWaitingQualityControl { get; }
    }
}
