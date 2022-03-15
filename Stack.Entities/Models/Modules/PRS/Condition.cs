using Stack.Entities.Models.Modules.RFQS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.PRS
{
    public class Condition
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionEN { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Level { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }


        //Navigation Properties . 
        public virtual List<PRMainItem_Condition> Condition_PRMainItems { get; set; }
        public virtual List<RFQMainItem_Condition> Condition_RFQMainItems { get; set; }


    }

}
