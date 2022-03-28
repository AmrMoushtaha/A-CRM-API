using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CR_Section
    {
        public long ID { get; set; }

        public int Order { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsSubmitted { get; set; }

        //Navigation Properties
        public long RequestID { get; set; }

        [ForeignKey("RequestID")]
        public virtual CustomerRequest CustomerRequest { get; set; }

        public long SectionID { get; set; }

        [ForeignKey("SectionID")]
        public virtual CRSection Section { get; set; }

        public virtual List<CRSectionQuestionAnswer> QuestionAnswers { get; set; }

    }

}
