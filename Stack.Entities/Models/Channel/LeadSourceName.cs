using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Channel
{
    public class LeadSourceName
    {
        public long ID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public int Status { get; set; }
        public string Icon { get; set; }
        public long LeadSourceTypeID { get; set; }

        [ForeignKey("LeadSourceTypeID")]
        public LeadSourceType LeadSourceType { get; set; }
    }

}
