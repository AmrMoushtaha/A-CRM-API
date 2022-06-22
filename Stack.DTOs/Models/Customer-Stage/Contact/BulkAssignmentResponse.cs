using Stack.DTOs.Models.Modules.Pool;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CustomerStage
{
    public class BulkAssignmentResponse
    {
        public bool Success { get; set; }
        public int RemainingSlots { get; set; }
    }
}
