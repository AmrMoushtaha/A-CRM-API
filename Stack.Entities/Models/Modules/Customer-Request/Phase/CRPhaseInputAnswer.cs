using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRPhaseInputAnswer
    {
        public long ID { get; set; }
        public long InputID { get; set; }
        public long RequestPhaseID { get; set; }
        public string Answer { get; set; }

        [ForeignKey("InputID")]
        public virtual CRPhaseInput Input { get; set; }

        [ForeignKey("RequestPhaseID")]
        public virtual CR_Timeline_Phase RequestPhase { get; set; }
    }

}
