using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.PRS
{
    public class PR
    {

        public long ID { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionEN { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }


        //Navigation Properties . 
        public int PRTypeID { get; set; }

        [ForeignKey("PRTypeID")]
        public virtual PRType PRType { get; set; }


    }

}
