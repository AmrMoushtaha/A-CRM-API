using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class SectionQuestion
    {
        public long ID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Type { get; set; }

        public bool isRequired { get; set; }

        public int Order { get; set; }

        public bool IsDecisional { get; set; }

        //Navigation Properties

        public long SectionID { get; set; }

        [ForeignKey("SectionID")]
        public virtual Section Section { get; set; }
        public virtual List<SectionQuestionOption> QuestionOptions { get; set; }
        public virtual List<SectionQuestionAnswer> QuestionAnswers { get; set; }

    }


}
