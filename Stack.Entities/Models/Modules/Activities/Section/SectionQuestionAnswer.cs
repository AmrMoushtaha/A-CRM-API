using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SectionQuestionAnswer
    {
        public long ID { get; set; }
        public long QuestionID { get; set; }
        public string Value { get; set; }

        //Navigation Properties . 
        public long? ActivitySectionID { get; set; }

        [ForeignKey("ActivitySectionID")]
        public virtual ActivitySection ActivitySection { get; set; }

        [ForeignKey("QuestionID")]
        public virtual SectionQuestion Question { get; set; }

    }

}
