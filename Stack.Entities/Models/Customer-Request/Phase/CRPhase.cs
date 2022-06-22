using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRPhase
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public virtual List<CRPhaseInput> Inputs { get; set; }
        public virtual List<CRTimeline_Phase> Timelines { get; set; }

    }

}
