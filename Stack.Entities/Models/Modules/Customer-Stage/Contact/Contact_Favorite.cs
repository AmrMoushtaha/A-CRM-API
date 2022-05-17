using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Contact_Favorite
    {
        public long ContactID { get; set; }
        public string UserID { get; set; }

        [ForeignKey("ContactID")]
        public virtual Contact Contact { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }

}
