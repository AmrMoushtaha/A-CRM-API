using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerRequest
{
    public class CRSubmissionDetails
    {
        public long ID { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public bool IsStatusChanged { get; set; }

        public string Comment { get; set; }

        public long RequestID { get; set; }

        [ForeignKey("RequestID")]
        public virtual CustomerRequest CustomerRequest { get; set; }



    }

}
