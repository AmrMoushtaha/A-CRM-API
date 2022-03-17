using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class SectionToAnswer
    {
        public long ID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public bool HasDecisionalQuestions { get; set; }
        public virtual List<QuestionToAnswer> Questions { get; set; }

    }

}
