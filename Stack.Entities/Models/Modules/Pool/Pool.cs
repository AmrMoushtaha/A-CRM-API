using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class Pool
    {
        public long ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public string ConfigurationType { get; set; }
        public int? Capacity { get; set; }

        public virtual List<Pool_Users> Pool_Users { get; set; }
        public virtual List<Pool_Admin> Pool_Admins { get; set; }
        public virtual List<Location_Pool> Location_Pools { get; set; }
        public virtual List<Contact> Contacts { get; set; }

    }

}
