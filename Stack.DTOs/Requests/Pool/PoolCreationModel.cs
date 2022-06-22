using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class PoolCreationModel
    {
        public string NameEN { get; set; }
        public string NameAR { get; set; }

        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public int ConfigurationType { get; set; }

        public int? Capacity { get; set; }

    }

}
