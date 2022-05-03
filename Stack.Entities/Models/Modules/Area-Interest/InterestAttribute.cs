﻿using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class InterestAttribute:BaseEntity
    {
        public string LabelAR { get; set; }
        public string LabelEN { get; set; }
        public virtual List<LInterest_InterestAttribute> LInterest_InterestAttributes { get; set; }


    }

}