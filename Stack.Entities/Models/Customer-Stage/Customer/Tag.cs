using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Tag
    {
        public long ID { get; set; }

        public string Title { get; set; }

        public virtual List<Customer_Tag> CustomerTags { get; set; }
        public virtual List<Contact_Tag> ContactTags { get; set; }
    }

}
