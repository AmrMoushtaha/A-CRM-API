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
        public int Type { get; set; }
        public long TimelineID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }


        [ForeignKey("TimelineID")]
        public CRPhaseTimeline PhasesTimeline { get; set; }

        public string InputsTemplate { get; set; } //Json Stringfied template model
        public List<CustomerRequest> CustomerRequests { get; set; }
    }

}
