using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class TeamMemberViewModel
    {
        public string UserID { get; set; }
        public string FullName { get; set; }

        public bool IsManager { get; set; }
        public string ManagerID { get; set; }
        public DateTime JoinDate { get; set; }

        public int Status { get; set; }

    }
}
