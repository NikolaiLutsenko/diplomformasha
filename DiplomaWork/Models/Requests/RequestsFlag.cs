using System;

namespace DiplomaWork.Models.Requests
{
    [Flags]
    public enum RequestsFlag
    {
        None = 0,
        Unnassigned = 1,
        IsNotWaitingQualityControl = 2
    }
}
