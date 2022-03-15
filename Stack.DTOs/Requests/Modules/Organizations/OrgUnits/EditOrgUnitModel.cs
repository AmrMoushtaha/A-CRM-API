using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Organizations.OrgUnits
{
    public class EditOrgUnitModel
    {
        public int ID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Code { get; set; }

        public long ManagerID { get; set; }

        public int CompanyCodeID { get; set; }

        public bool IsPurchasing { get; set; }

    }

}
