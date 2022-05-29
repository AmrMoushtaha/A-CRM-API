using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class CRTimelineCreationModel
    {
        public long? ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public List<Timeline_PhaseLinkModel> Phases { get; set; }
    }


    public class Timeline_PhaseLinkModel
    {
        public long PhaseID { get; set; }

        public long? ParentPhaseID { get; set; }

    }

}
