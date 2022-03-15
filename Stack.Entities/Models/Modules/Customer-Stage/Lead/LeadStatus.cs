using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class LeadStatus
    {
        public long ID { get; set; }

        public string EN { get; set; }
        public string AR { get; set; }
        public string Status { get; set; }
        public long LeadID { get; set; }


        public virtual List<Lead> Leads { get; set; }

    }

}
