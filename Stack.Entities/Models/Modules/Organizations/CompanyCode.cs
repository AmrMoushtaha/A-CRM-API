﻿using Stack.Entities.Models.Modules.Materials;
using Stack.Entities.Models.Modules.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Organizations
{
    public class CompanyCode
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string DescriptionAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string DescriptionEN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime? Update_Date { get; set; }

        //Navigation Properties 
        public virtual List<CostCenter> CostCenters { get; set; }

        public virtual List<Plant> Plants { get; set; }

        public virtual List<Vendor_CompanyCode> CompanyCode_Vendors { get; set; }

    }

}
