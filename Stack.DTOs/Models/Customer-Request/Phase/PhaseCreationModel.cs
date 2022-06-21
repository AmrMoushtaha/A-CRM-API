using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.CR
{
    public class PhaseCreationModel
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public List<PhaseInputCreationModel> PhaseInputs { get; set; }
    }


    public class PhaseInputCreationModel
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public int Type { get; set; }
        public List<PhaseAttributeCreationModel> Attributes { get; set; }
    }

    public class PhaseAttributeCreationModel
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
    }

}
