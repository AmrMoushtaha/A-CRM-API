using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Customer_Tag
    {
        public long CustomerID { get; set; }
        public long TagID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }

}
