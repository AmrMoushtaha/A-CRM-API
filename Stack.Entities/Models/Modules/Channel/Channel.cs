using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Channel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Channels
{
    public class Channel
    {
        public long ID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public string Icon { get; set; }
        public int Status { get; set; }
        public List<LeadSourceType> LeadSourceType { get; set; }
    }

}
