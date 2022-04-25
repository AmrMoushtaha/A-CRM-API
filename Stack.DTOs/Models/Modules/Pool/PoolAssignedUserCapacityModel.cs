using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolAssignedUserCapacityModel
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public int Capacity { get; set; }
        public int OccupiedSlots { get; set; }

    }


}
