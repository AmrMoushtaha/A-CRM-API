using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities.ActivityDetails
{
    public class ActivitySectionDetailsDTO
    {

            public long Order { get; set; }

            public List<SectionQuestionAnswerDetailsDTO> QuestionAnswers { get; set; }

    }

}
