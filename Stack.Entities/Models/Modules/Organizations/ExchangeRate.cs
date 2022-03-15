using Stack.Entities.Models.Modules.Materials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Organizations
{
    public class ExchangeRate
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string FirstCurrency { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string SecondCurrency { get; set; }

        public float Rate { get; set; }


        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }


        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime? Update_Date { get; set; }


        //Navigation Properties . 
        public int CompanyCodeID { get; set; }

        [ForeignKey("CompanyCodeID")]
        public virtual CompanyCode CompanyCode { get; set; }

    }

}
