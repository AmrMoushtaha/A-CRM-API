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
        public string UniqueNumber { get; set; }
        public long? DealID { get; set; }
        public long? ContactID { get; set; }
        public long? InterestID { get; set; }
        public long TimelineID { get; set; }
        public long RequestTypeID { get; set; }
        public int TypeIndex { get; set; }

        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }

        [ForeignKey("RequestTypeID")]
        public virtual CustomerRequestType RequestType { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

        public virtual List<CR_Timeline> Timeline { get; set; }

    }

}
