using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Pool
{
    public class PoolConfigurationModel
    {
        public long PoolID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public int ConfigurationType { get; set; }
        public int? Capacity { get; set; }
        public int UsersCount { get; set; }
        public int AdminsCount { get; set; }
        public int RequestsCount { get; set; }

    }


}
