using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Materials.Plants
{
    public class EditPlantModel
    {
        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string Code { get; set; }

        public int PlantID { get; set; }

        public int CompanyCodeID { get; set; }

    }

}
