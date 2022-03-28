using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRSectionQuestionOption
    {

        public long ID { get; set; }
  
        public string ValueAR { get; set; }

        public string ValueEN { get; set; }

        public string RoutesTo { get; set; } 


        //Navigation Properties . 
        public long QuestionID { get; set; }

        [ForeignKey("QuestionID")]
        public virtual CRSectionQuestion Question { get; set; }

        public virtual List<CRSelectedOption> SelectedOptions { get; set; }


    }

}
  