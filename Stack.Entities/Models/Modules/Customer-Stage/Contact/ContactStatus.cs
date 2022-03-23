using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class ContactStatus
    {
        public long ID { get; set; }
        public string EN { get; set; }
        public string AR { get; set; }
        public int Status { get; set; }

        public virtual List<Contact> Contacts { get; set; }
    }

}
