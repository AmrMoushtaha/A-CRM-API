using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Employees
{
    public class Employee_Org_Unit
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        //Navigation Properties.
        public long EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        public virtual int Org_Unit_ID { get; set; }

        [ForeignKey("Org_Unit_ID")]

        public virtual OrgUnit OrgUnit { get; set; }

    }

}
