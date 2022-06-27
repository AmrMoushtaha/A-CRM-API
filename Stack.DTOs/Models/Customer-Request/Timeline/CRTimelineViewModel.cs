using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class CRTimelineViewModel
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public List<PhaseViewModel> Phases { get; set; }
    }

    public class TimelineViewModel
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public List<RequestPhaseViewModel> Phases { get; set; }
    }
}
