using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class ActivitySection
    {
        public long ID { get; set; }

        //Navigation Properties {get;set;}
        public long ActivityID { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }

        public long SectionID { get; set; }

        [ForeignKey("SectionID")]
        public virtual Section Section { get; set; }

        public virtual List<SectionQuestionAnswer> QuestionAnswers { get; set; }

    }

}
