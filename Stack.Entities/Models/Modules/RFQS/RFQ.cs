using Stack.Entities.Models.Modules.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.RFQS
{
    public class RFQ
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
        public int RFQTypeID { get; set; }

        [ForeignKey("RFQTypeID")]
        public virtual RFQType RFQType { get; set; }

        public long VendorID { get; set; }

        [ForeignKey("VendorID")]
        public virtual Vendor Vendor { get; set; }


    }

}
