using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class QuestionToAnswer
    {
        public long ID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Answer { get; set; } // Holds the answer value for questions that do no have options . 

        public string Type { get; set; }

        public bool isRequired { get; set; }

        public bool IsDecisional { get; set; }

        public int Order { get; set; }

        public List<QuestionOption> Options { get; set; }


    }

}
