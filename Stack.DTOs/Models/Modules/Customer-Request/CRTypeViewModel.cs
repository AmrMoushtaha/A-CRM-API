using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class CRTypeViewModel
    {
        public long ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int Type { get; set; }
        public long TimelineID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        public CRTimelineViewModel Timeline { get; set; }
    }
}
