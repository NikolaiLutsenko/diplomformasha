namespace DiplomaWork1.Models.Requests
{
    public class ShowBadgeModel
    {
        public ShowBadgeModel(bool isCompleted, bool isReturned, bool isWaitingQualityControl)
        {
            IsCompleted = isCompleted;
            IsReturned = isReturned;
            IsWaitingQualityControl = isWaitingQualityControl;
        }

        public bool IsCompleted { get; }

        public bool IsReturned { get; }

        public bool IsWaitingQualityControl { get; }
    }
}
