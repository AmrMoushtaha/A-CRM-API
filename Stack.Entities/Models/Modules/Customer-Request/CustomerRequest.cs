using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CustomerRequest
    {
        //Details
        public long ID { get; set; }
        public int Type { get; set; }
        public long DealID { get; set; }
        public long ContactID { get; set; }
        public long TimelineID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("RequestTypeID")]
        public CustomerRequestType RequestType { get; set; }

        [ForeignKey("DealID")]
        public Deal Deal { get; set; }

        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

        [ForeignKey("TimelineID")]
        public CRPhaseTimeline PhasesTimeline { get; set; }

        public List<CustomerRequestInput> RequestInputs { get; set; }
        //public List<CustomerRequestInputAnswer> Answers { get; set; }
    }

}
