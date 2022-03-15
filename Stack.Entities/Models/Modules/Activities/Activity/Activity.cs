﻿using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class Activity
    {
        public long ID { get; set; }
        public long ProcessFlowID { get; set; }
        public long ActivityTypeID { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedBy { get; set; }

        //Navigation Properties .
        public virtual List<ActivitySection> ActivitySections { get; set; }

        [ForeignKey("ProcessFlowID")]
        public virtual ProcessFlow ProcessFlow { get; set; }

        [ForeignKey("ActivityTypeID")]
        public virtual ActivityType ActivityType { get; set; }

    }

}
