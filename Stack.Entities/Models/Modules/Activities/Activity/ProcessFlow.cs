using Stack.Entities.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Activities
{
    public class ProcessFlow
    {
        public long ID { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsComplete {get;set;} // Default value equals false . 

        //Navigation Properties 

        public long CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public long? DealID { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }

        public virtual List<Activity> Activities { get; set; }

        public long ContactID { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }


    }

}
