using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Initialization.ActivityTypes
{
    public class QuestionModel
    {
        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Type { get; set; }

        public int Order { get; set; }

        public bool IsRequired { get; set; }

        public bool IsDecisional { get; set; }

        public  List<QuestionOptionModel> QuestionOptions { get; set; }

    }

}
