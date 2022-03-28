using System.Collections.Generic;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRType
    {
        public long ID { get; set; }

        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        //Navigation Properties . 

        public virtual List<CustomerRequest> Requests { get; set; }

        public virtual List<CRSubmissionDetails> SubmissionDetails { get; set; }

        public virtual List<CRSection> Sections { get; set; }

    }

}
