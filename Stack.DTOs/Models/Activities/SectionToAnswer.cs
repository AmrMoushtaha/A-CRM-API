using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class SectionToAnswer
    {
        public long ID { get; set; } // Activity Section ID .
        public bool IsFinalSection { get; set; } // True when it's time to submit the activity . 


        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public int Order { get; set; } // Activity sections's order .
        public int ActivityTypeSectionOrder { get; set; }
        public bool HasCreateIntrest { get; set; }
        public bool HasCreateRequest { get; set; }
        public bool HasCreateResale { get; set; }

        // Activity type section's order . 
        public long ActivityID { get; set; }
        public long ActivityTypeID { get; set; }
        public bool HasDecisionalQuestions { get; set; }
        public virtual List<QuestionToAnswer> Questions { get; set; }

    }

}
