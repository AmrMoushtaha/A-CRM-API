using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Materials.MaterialTypes
{
    public class MaterialTypesMainViewDTO
    {
        public int ID { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public string Code { get; set; }
        public DateTime Creation_Date { get; set; }

    }

}
