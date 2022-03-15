using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Materials.Plants
{
    public class PlantMainViewDTO
    {
        public int ID { get; set; }

        public int CompanyCodeID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Code { get; set; }

        public string CompanyCodeDescriptionEN { get; set; }

        public string CompanyCodeDescriptionAR { get; set; }

        public string CompanyCode { get; set; }


    }
}
