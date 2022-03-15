using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Employees
{
    public class Employee_Position
    {

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public DateTime StartDate { get; set; }

        public double Percentage { get; set; }

        //Navigation Properties . 
        public long EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int PositionID { get; set; }
        public virtual Position Position { get; set; }


    }

}
