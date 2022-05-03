using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Enums
{
    public enum ErrorType
    {
        LogicalError,
        SystemError,
        Request,
        Warning,
        NotFound,
        IncreaseCapacity,
        ReselectUser,
        CapacityReached,
        Record_DifferentPool,
        Record_DifferentPool_Admin
    }
}
