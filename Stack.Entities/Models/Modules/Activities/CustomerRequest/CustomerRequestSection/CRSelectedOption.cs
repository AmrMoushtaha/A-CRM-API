using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRSelectedOption
    {

        public long ID { get; set; }

        //Navigation Properties . 
        public long? SectionQuestionOptionID { get; set; }

        [ForeignKey("SectionQuestionOptionID")]
        public virtual CRSectionQuestionOption SectionQuestionOption { get; set; }

        public long? SectionQuestionAnswerID { get; set; }

        [ForeignKey("SectionQuestionAnswerID")]
        public virtual CRSectionQuestionAnswer SectionQuestionAnswer { get; set; }


    }

}
  