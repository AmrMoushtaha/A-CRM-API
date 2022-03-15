using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Materials.MaterialTypes
{
    public class EditMaterialTypeModel
    {
        public long ID { get; set; }
        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string Code { get; set; }


    }

}

