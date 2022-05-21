using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRPhaseTimeline
    {
        public long ID { get; set; }
        public int Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public List<CustomerRequest> CustomerRequests { get; set; }

        public List<CRTimeline_Phase> Phases { get; set; }
    }

}
