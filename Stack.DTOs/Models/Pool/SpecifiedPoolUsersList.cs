using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class SpecifiedPoolUser
    {
        public string UserID { get; set; }
        public int? AssignedRecordsCount { get; set; }
        public int? UserPoolCapacity { get; set; }
        public int Index { get; set; }
    }
}
