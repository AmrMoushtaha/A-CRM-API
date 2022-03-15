using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models.Modules.Materials
{
    public class Material
    {
        public long ID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string DescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionEN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string EAN { get; set; }

        [Column(TypeName = "varchar(1)")]
        public string PriceIndicator { get; set; }

        public long Stock { get; set; }

        public float MovingAverage { get; set; }

        public float StandardPrice { get; set; }

        public float MinimumOrderAmount { get;set; }

        public float SafetyStock { get;set; }

        public float MaxStock { get;set; }

        public float ReOrderPoint { get;set; }

        public float LeadTime { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties 
        public int MaterialTypeID { get; set; }

        [ForeignKey("MaterialTypeID")]
        public virtual MaterialType MaterialType { get; set; }

        public int MaterialGroupID { get; set; }

        [ForeignKey("MaterialGroupID")]
        public virtual MaterialGroup MaterialGroup { get; set; }

        public virtual List<Material_UOM> Material_UOMS { get; set; }

        public virtual List<Plant_Material> Material_Plants { get; set; }

    }

}
