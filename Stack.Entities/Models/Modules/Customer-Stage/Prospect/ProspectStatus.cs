﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.CustomerStage
{
    public class ProspectStatus
    {
        public long ID { get; set; }

        public string EN { get; set; }
        public string AR { get; set; }
        public string Status { get; set; }

        public virtual List<Prospect> Prospects { get; set; }


    }

}
