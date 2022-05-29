using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CustomerRequestType
    {
        public long ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public long TimelineID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("TimelineID")]
        public virtual CRTimeline PhasesTimeline { get; set; }

        public virtual List<CustomerRequest> CustomerRequests { get; set; }
    }

}
