﻿using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Opportunity_Favorite
    {
        public long RecordID { get; set; }
        public string UserID { get; set; }

        [ForeignKey("RecordID")]
        public virtual Opportunity Record { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }

}
