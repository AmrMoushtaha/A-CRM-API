using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Organizations.OrgUnits
{
    public class OrgUnitDTO
    {
        public int ID { get; set; }


        public string DescriptionAR { get; set; }

     
        public string DescriptionEN { get; set; }

        public DateTime CreationDate { get; set; }

        public long ManagerId { get; set; }

        public bool IsPurchasing { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

  
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }


    }

}
