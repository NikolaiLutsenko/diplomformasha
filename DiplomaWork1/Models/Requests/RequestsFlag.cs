using System;

namespace DiplomaWork1.Models.Requests
{
    [Flags]
    public enum RequestsFlag
    {
        None = 0,
        Unnassigned = 1,
        IsNotWaitingQualityControl = 2
    }
}
