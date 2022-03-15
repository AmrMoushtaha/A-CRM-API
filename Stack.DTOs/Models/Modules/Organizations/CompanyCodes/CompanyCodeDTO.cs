using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Organizations.CompanyCodes
{
    public class CompanyCodeDTO
    {
        public int ID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Code { get; set; }

        public string Currency { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Updated_By { get; set; }

        public DateTime? Update_Date { get; set; }

    }

}
