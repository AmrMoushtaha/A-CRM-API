using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Organizations.OrgUnits
{
    public class OrgUnitsMainViewDTO
    {
        public int ID { get; set; }

        public string DescriptionAR { get; set; }
     
        public string DescriptionEN { get; set; }

        public string Code { get; set; }

        public long ManagerId { get; set; }

        public string ManagerNameAR { get; set; }

        public string ManagerNameEN { get; set; }

        public string CompanyCode { get; set; }

        public bool IsPurchasing { get; set; }

        public string Created_By { get; set; }

        public string Updated_By { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Creation_Date { get; set; }

        public DateTime Update_Date { get; set; }

    }

}
