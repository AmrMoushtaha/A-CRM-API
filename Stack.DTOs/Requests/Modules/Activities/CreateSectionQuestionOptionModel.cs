using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Activities
{
    public class CreateSectionQuestionOptionModel
    {

        public long QuestionID { get; set; }

        public string ValueEN { get; set; }

        public string ValueAR { get; set; }

        public long RoutesTo { get; set; } // if value equals Submit - Return the submit section as the upcoming section . 

    }

}
