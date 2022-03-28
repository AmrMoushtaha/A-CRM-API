using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CustomerRequest
    {
        public long ID { get; set; }

        public long ProcessFlowID { get; set; }

        public long TypeID { get; set; }

        public DateTime? CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? SubtmissionDate { get; set; }


        public bool IsSubmitted { get; set; }

        //Navigation Properties .
        public virtual List<CR_Section> RequestSections { get; set; }

        public virtual CRSubmissionDetails SubmissionDetails { get; set; }

        [ForeignKey("TypeID")]
        public virtual CRType RequestType { get; set; }

    }

}
