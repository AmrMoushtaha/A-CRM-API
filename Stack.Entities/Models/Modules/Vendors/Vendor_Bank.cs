using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Vendors
{
    public class Vendor_Bank
    {

        [Column(TypeName = "varchar(100)")]
        public string IBAN { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string AccountID { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SwiftCode { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        //Navigation Properties . 
        public long VendorID { get; set; }
        public virtual Vendor Vendor { get; set; }

        public int BankID { get; set; }
        public virtual Bank Bank { get; set; }


    }

}
