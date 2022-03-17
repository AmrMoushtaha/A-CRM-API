using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SectionQuestionOption
    {

        public long ID { get; set; }
  
        public string ValueAR { get; set; }

        public string ValueEN { get; set; }

        public long RoutesTo { get; set; } 


        //Navigation Properties . 
        public long QuestionID { get; set; }

        [ForeignKey("QuestionID")]
        public virtual SectionQuestion Question { get; set; }

        public virtual List<SelectedOption> SelectedOptions { get; set; }


    }

}
  