using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Organizations.PurchasingGroups
{
    public class EditPGModel
    {
        public int ID { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Code { get; set; }

        public int PlantID { get; set; }

    }

}
