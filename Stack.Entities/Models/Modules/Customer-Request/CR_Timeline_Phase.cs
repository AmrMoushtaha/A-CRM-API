using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CR_Timeline_Phase
    {
        public long ID { get; set; }
        public long RequestID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public long TimelinePhaseID { get; set; }

        [ForeignKey("TimelinePhaseID")]
        public virtual CRTimeline_Phase Timeline_Phase { get; set; }

        public virtual List<CRPhaseInputAnswer> Answers { get; set; }

    }

}
