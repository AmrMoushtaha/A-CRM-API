using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CR_Timeline
    {
        public long ID { get; set; }
        public long TimelineID { get; set; }
        public long RequestID { get; set; }

        [ForeignKey("RequestID")]
        public virtual CustomerRequest CustomerRequest { get; set; }

        [ForeignKey("TimelineID")]
        public virtual CRTimeline Timeline { get; set; }

        //public virtual List<CR_Timeline_Phase> RequestTimelinePhases { get; set; }
    }

}
