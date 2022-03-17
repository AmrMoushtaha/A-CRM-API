using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class SectionToAnswer
    {
        public long ID { get; set; } // Activity Section ID .
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public int Order { get; set; } // Activity sections's order .
        public int ActivityTypeSectionOrder { get; set; } // Activity type section's order . 
        public long ActivityID { get; set; }
        public long ActivityTypeID { get; set; }
        public bool IsSubmitSection { get; set; }
        public bool HasDecisionalQuestions { get; set; }
        public virtual List<QuestionToAnswer> Questions { get; set; }

    }

}
