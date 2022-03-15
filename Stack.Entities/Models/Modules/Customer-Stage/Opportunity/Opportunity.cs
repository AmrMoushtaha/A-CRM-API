﻿using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Opportunity
    {
        public long ID { get; set; }
        public string AssignedUserID { get; set; }
        public long DealID { get; set; }

        public string State { get; set; }
        public long StatusID { get; set; }
        public bool isJunked { get; set; }

        [ForeignKey("StatusID")]
        public virtual OpportunityStatus Status { get; set; }

        [ForeignKey("AssignedUserID")]
        public virtual ApplicationUser AssignedUser { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }
    }

}
