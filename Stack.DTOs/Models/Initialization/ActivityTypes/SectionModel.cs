using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Initialization.ActivityTypes
{
    public class SectionModel
    {
        public string Reference { get; set; }

        public string Id { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public int Order { get; set; }
        public bool HasDecisionalQuestions { get; set; }
        public bool HasCreateInterest { get; set; }
        public bool HasCreateRequest { get; set; }
        public bool HasCreateResale { get; set; }

        public  List<QuestionModel> Questions { get; set; }

    }

}
