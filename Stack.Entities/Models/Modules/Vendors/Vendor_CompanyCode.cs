using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Vendors
{
    public class Vendor_CompanyCode
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }


        //Navigation Properties . 
        public long VendorID { get; set; }
        public virtual Vendor Vendor { get; set; }

        public int CompanyCodeID { get; set; }
        public virtual CompanyCode CompanyCode { get; set; }

    }

}
