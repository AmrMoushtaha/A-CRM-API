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
        //public string DescriptionAR { get; set; }
        //public string DescriptionEN { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public List<CRPhaseInput> Inputs { get; set; }

        public List<CRTimeline_Phase> Timelines { get; set; }
    }

}
