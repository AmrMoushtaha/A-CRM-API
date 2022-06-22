using Stack.Entities.Models.Modules.Areas;
using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Common
{
    public class SystemConfiguration
    {
        public int ID { get; set; }
        public double LockDuration { get; set; }
        public double AutomaticUnassignmentDuration { get; set; }

    }

}
