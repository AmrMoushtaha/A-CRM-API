using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.PRS
{
    public class PRSubItem
    {

        public long ID { get; set; }

        public long ServiceID { get; set; }

        public int UOMID { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ServiceDescriptionEN { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ServiceDescriptionAR { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UOMAbbreviation { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        public int LineItemNumber { get; set; }

        public float Price { get; set; }

        public float Quantity { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }


        //Navigation Properties .

        public long PRMainItemID { get; set; }

        [ForeignKey("PRMainItemID")]
        public virtual PRMainItem PRMainItem { get; set; }





    }

}
