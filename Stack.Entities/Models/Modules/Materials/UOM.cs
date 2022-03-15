using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Materials
{
    public class UOM
    {

        public int ID { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Abbreviation { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        public virtual List<Material_UOM> UOM_Materials { get; set; }


    }

}
