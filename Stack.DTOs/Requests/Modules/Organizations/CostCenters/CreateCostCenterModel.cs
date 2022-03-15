using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Organizations.CostCenters
{
    public class CreateCostCenterModel
    {
        public int CompanyCodeID { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }
        public string Code { get; set; }

    }

}
