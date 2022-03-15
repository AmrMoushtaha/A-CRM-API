using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Vendors
{
    public class VendorAddress
    {

        public long ID { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Street { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Governorate { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string District { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string BuildingNumber { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string OtherInfo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties . 
        public long VendorID { get; set; }

        [ForeignKey("VendorID")]
        public virtual Vendor Vendor { get; set; }

    }

}
