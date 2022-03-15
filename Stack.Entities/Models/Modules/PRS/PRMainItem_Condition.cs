using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.PRS
{
    public class PRMainItem_Condition
    {

        [Column(TypeName = "varchar(10)")]
        public string Per { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        public float TotalValue { get; set; }
        public float Price { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        //Navigation Properties . 
        public long PRMainItemID { get; set; }
        public virtual PRMainItem PRMainItem { get; set; }

        public int ConditionID { get; set; }
        public virtual Condition Condition { get; set; }


    }

}
