using Stack.Entities.Models.Modules.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.AreaInterest
{
    public class InterestAttribute
    {
        public long ID { get; set; }
        public string Label { get; set; }


        public virtual List<LOneInterest_InterestAttributes> LevelOne { get; set; }
        public virtual List<LTwoInterest_InterestAttributes> LevelTwo { get; set; }
        public virtual List<LThreeInterest_InterestAttributes> LevelThree { get; set; }


    }

}
