using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities.ActivityDetails
{
    public class SectionQuestionAnswerDetailsDTO
    {

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string QuestionType { get; set; }
        public string AnswerValueEN { get; set; }
        public string AnswerValueAR { get; set; }
        public DateTime AnswerDateValue { get; set; }
        public long Order { get; set; }


    }

}
