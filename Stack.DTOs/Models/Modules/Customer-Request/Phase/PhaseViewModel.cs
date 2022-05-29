using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class PhaseViewModel
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public int Status { get; set; }
        public long? ParentPhaseID { get; set; }
        public long PhaseID { get; set; }
        public List<PhaseInputViewModel> Inputs { get; set; }
    }


    public class PhaseInputViewModel
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public int Type { get; set; }
        public List<PhaseAttributeViewModel> Attributes { get; set; }
        public List<PhaseInputAnswerViewModel> Answers { get; set; }
    }

    public class PhaseAttributeViewModel
    {
        public long ID { get; set; }
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
    }
    public class PhaseInputAnswerViewModel
    {
        public long ID { get; set; }
        public long InputID { get; set; }
        public string Answer { get; set; }
    }

}
