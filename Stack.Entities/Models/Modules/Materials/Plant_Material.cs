using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Materials
{
    public class Plant_Material
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        //Navigation Properties . 
        public long MaterialID { get; set; }
        public virtual Material Material { get; set; }

        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }

    }

}
