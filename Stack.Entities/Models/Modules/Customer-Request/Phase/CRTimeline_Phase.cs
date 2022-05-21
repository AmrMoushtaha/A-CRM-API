using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRTimeline_Phase
    {
        public long PhaseID { get; set; }
        public long? ParentPhaseID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TimelineID { get; set; }

        [ForeignKey("PhaseID")]
        public CRPhase Phase { get; set; }

        [ForeignKey("TimelineID")]
        public CRPhaseTimeline Timeline { get; set; }
    }

}
