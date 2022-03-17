using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Activities
{
    public class QuestionOption
    {

        public long ID { get; set; }

        public string ValueAR { get; set; }

        public string ValueEN { get; set; }

        public long RoutesTo { get; set; }

        public bool IsSelected { get; set; }



    }

}
