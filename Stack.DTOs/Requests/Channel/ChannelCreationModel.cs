using Stack.DTOs.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.CustomerStage
{
    public class ChannelCreationModel
    {
        public long? parentID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public int Status { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

    }

}
