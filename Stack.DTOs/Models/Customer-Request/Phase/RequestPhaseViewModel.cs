using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class RequestPhaseViewModel
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public int Status { get; set; }
        public long? ParentPhaseID { get; set; }
        public long PhaseID { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<PhaseInputViewModel> Inputs { get; set; }
    }



}
