using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Organizations.PurchasingGroups
{
    public class CreatePGModel
    {
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Code { get; set; }
        public int PlantId { get; set; }

    }

}
