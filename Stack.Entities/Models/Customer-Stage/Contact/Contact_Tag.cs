using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Contact_Tag
    {
        public long ContactID { get; set; }
        public long TagID { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }

}
