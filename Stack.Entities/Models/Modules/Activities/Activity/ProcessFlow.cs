using Stack.Entities.Models.Modules.CustomerStage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class ProcessFlow
    {
        public long ID { get; set; }

        //Navigation Properties 

        public long CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public long? DealID { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }

        public virtual List<Activity> Activities { get; set; }

    }

}
