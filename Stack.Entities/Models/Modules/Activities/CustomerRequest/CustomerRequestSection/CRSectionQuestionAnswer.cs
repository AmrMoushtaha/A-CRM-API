using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRSectionQuestionAnswer
    {
        public long ID { get; set; }

        public string Value { get; set; }

        //Navigation Properties . 
        public long? RequestSectionID { get; set; }

        [ForeignKey("RequestSectionID")]
        public virtual CRSection RequestSection { get; set; }

        public long? QuestionID { get; set; }

        [ForeignKey("QuestionID")]
        public virtual CRSectionQuestion Question { get; set; }


        public virtual CRSelectedOption SelectedOption { get; set; }


    }

}
