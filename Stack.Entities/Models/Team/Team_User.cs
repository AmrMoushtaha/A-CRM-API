using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Teams
{
    public class Team_User
    {
        public string UserID { get; set; }
        public long TeamID { get; set; }

        public bool IsManager { get; set; }

        public int Status { get; set; }

        public DateTime JoinDate { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TeamID")]
        public virtual Team Team { get; set; }

    }

}
