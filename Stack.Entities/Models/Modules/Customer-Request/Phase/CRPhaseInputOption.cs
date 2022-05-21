using Stack.Entities.Models.Modules.Activities;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CR
{
    public class CRPhaseInputOption
    {
        public long ID { get; set; }
        public long InputID { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        [ForeignKey("InputID")]
        public CRPhaseInput PhaseInput { get; set; }
    }

}
