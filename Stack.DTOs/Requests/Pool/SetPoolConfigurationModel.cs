using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Pool
{
    public class SetPoolConfigurationModel
    {
        public long PoolID { get; set; }
        public int ConfigurationType { get; set; }
        public int? Capacity { get; set; }

    }

}
