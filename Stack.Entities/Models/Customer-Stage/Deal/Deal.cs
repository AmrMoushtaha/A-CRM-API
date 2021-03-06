using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.CR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Deal
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }

        public long ActiveStageID { get; set; }

        public int ActiveStageType { get; set; }

        public long PoolID { get; set; }

        public virtual List<Prospect> Prospects { get; set; }
        public virtual List<Lead> Leads { get; set; }
        public virtual List<Opportunity> Opportunities { get; set; }
        public virtual List<DoneDeal> DoneDeals { get; set; }
        public virtual List<CustomerRequest> CustomerRequests { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public virtual ProcessFlow ProcessFlow { get; set; }
    }

}
