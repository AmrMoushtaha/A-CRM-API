using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SelectedOption
    {

        public long ID { get; set; }

        //Navigation Properties . 
        public long SectionQuestionOptionID { get; set; }

        [ForeignKey("SectionQuestionOptionID")]
        public virtual SectionQuestionOption SectionQuestionOption { get; set; }

        public long SectionQuestionAnswerID { get; set; }

        [ForeignKey("SectionQuestionAnswerID")]
        public virtual SectionQuestionAnswer SectionQuestionAnswer { get; set; }


    }

}
  