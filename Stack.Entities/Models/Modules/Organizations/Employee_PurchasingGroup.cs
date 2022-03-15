using Stack.Entities.Models.Modules.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Organizations
{
    public class Employee_PurchasingGroup
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        //Navigation Properties . 
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int PurchasingGroupID { get; set; }
        public virtual PurchasingGroup PurchasingGroup { get; set; }


    }

}
