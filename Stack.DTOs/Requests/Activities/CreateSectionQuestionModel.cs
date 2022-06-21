using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class CreateSectionQuestionModel
    {

        public long SectionID { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDecisional { get; set; }

    }

}
