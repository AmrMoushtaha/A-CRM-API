using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolAssignedUsersModel
    {
        public string UserID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime JoinDate { get; set; }

        public int Status { get; set; }

        public int? Capacity { get; set; }
        public int? ReservedSlots { get; set; }
    }
}
