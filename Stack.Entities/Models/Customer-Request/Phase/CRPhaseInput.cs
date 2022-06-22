using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRPhaseInput
    {
        public long ID { get; set; }
        public long PhaseID { get; set; }
        public int Type { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        [ForeignKey("PhaseID")]
        public virtual CRPhase Phase { get; set; }

        public virtual List<CRPhaseInputOption> Options { get; set; }
        public virtual List<CRPhaseInputAnswer> Answers { get; set; }

    }

}
