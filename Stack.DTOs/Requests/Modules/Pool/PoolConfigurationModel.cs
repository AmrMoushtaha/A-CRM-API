using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class PoolConfigurationModel
    {
        public long PoolID { get; set; }
        public string ConfigurationType { get; set; }
        public int? Capacity { get; set; }

    }

}
