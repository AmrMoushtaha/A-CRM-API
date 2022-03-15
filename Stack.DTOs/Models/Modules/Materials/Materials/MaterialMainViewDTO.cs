using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Materials.MaterialTypes
{
    public class MaterialMainViewDTO
    {
        public long ID { get; set; }

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string Code { get; set; }

        public string EAN { get; set; }

        public string TypeIndicator { get; set; }

        public string PriceIndicator { get; set; }

        public long Stock { get; set; }

        public float MovingAverage { get; set; }

        public float StandardPrice { get; set; }

        public float MininmumOrderAmount { get; set; }

        public float SafetyStock { get; set; }

        public float MaxStock { get; set; }

        public float ReOrderPoint { get; set; }

        public float LeadTime { get; set; }

        public long MaterialGroupID { get; set; }
        public string MaterialGroupCode { get; set; }

        public long MaterialTypeID { get; set; }
        public string MaterialTypeCode { get; set; }
    }

}
