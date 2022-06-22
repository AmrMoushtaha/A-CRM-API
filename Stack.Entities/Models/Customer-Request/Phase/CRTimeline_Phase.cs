using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRTimeline_Phase
    {
        public long ID { get; set; }
        public long PhaseID { get; set; }
        public long? ParentPhaseID { get; set; }

        public long TimelineID { get; set; }

        [ForeignKey("PhaseID")]
        public virtual CRPhase Phase { get; set; }

        [ForeignKey("TimelineID")]
        public virtual CRTimeline Timeline { get; set; }

        public virtual List<CR_Timeline_Phase> RequestTimelinePhaseDetails { get; set; }

    }

}
