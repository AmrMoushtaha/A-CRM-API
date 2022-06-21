using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRTimeline
    {
        public long ID { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public int Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual List<CustomerRequestType> CustomerRequestTypes { get; set; }
        public virtual List<CR_Timeline> CustomerRequests { get; set; }
        public virtual List<CRTimeline_Phase> Phases { get; set; }
    }

}
