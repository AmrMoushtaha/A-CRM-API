using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Materials
{
    public class StorageLocation
    {

        public int ID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string DescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionEN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties . 
        public int PlantID { get; set; }

        [ForeignKey("PlantID")]
        public virtual Plant Plant { get; set; }

    }

}
