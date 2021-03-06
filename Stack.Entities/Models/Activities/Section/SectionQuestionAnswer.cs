using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SectionQuestionAnswer
    {
        public long ID { get; set; }

        public string Value { get; set; }

        public DateTime DateValue { get; set; }

        //Navigation Properties . 
        public long? ActivitySectionID { get; set; }

        [ForeignKey("ActivitySectionID")]
        public virtual ActivitySection ActivitySection { get; set; }

        public long? QuestionID { get; set; }

        [ForeignKey("QuestionID")]
        public virtual SectionQuestion Question { get; set; }


        public virtual SelectedOption SelectedOption { get; set; }


    }

}
