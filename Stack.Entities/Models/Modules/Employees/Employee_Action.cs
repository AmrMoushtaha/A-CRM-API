using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Employees
{
    public class Employee_Action
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        //Navigation Properties . 
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int ActionID { get; set; }
        public virtual Action Action { get; set; }

    }

}
