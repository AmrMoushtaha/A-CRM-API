using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Materials.Material
{
    public class MaterialCreationModel
    {

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string Code { get; set; }

        public string EAN { get; set; }

        public string PriceIndicator { get; set; }

        public long Stock { get; set; }

        public float MovingAverage { get; set; }

        public float StandardPrice { get; set; }

        public float MinimumOrderAmount { get; set; }

        public float SafetyStock { get; set; }

        public float MaxStock { get; set; }

        public float ReOrderPoint { get; set; }

        public float LeadTime { get; set; }

        public int MaterialTypeID { get; set; }

        public int MaterialGroupID { get; set; }

        public List<long>? Material_UOMIds { get; set; }
    }

}

