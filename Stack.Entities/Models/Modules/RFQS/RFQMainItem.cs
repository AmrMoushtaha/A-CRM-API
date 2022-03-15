using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.RFQS
{
    public class RFQMainItem
    {

        public long ID { get; set; }

        public long MaterialID { get; set; }

        public int PlantID { get; set; }

        public int UOMID { get; set; }

        public int MaterialGroupID { get; set; }

        public int PurchasingGroupID { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string PurchasingGroupDescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string PurchasingGroupDescriptionEN { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MaterialDescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MaterialDescriptionEN { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MaterialGroupDescriptionEN { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MaterialGroupDescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string PlantDescriptionEN { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string PlantDescriptionAR { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UOMAbbreviation { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string TypeIndicator { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int LineItemNumber { get; set; }

        public float ValuationPrice { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }


        //Navigation Properties .
        public long RFQID { get; set; }

        [ForeignKey("RFQID")]
        public virtual RFQ RFQ { get; set; }

        public virtual List<RFQSubItem> SubItems { get; set; }

        public virtual List<RFQMainItem_Condition> MainItem_Conditions { get; set; }


    }

}
