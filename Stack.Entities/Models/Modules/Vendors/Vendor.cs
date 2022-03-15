using Stack.Entities.Models.Modules.RFQS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Vendors
{
    public class Vendor
    {

        public long ID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NameAR { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NameEN { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string SystemUsername { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TaxRegistrationNumber { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties .

        public virtual List<VendorAddress> VendorAddresses { get; set; }
        public virtual List<VendorEmail> VendorEmails { get; set; }
        public virtual List<VendorPhoneNumber> VendorPhoneNumbers { get; set; }
        public virtual List<RFQ> RFQS { get; set; }
        public virtual List<Vendor_Tax> Vendor_Taxes { get; set; }
        public virtual List<Vendor_Bank> Vendor_Banks { get; set; }
        public virtual List<Vendor_CompanyCode> Vendor_CompanyCodes { get; set; }


    }

}
