using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Employees
{
    public class PositionAssignment
    {
        public int ID { get; set; }

        public long EmployeeID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string EmployeeNameAR { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string EmployeeNameEN { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties . 

        public int PositionID { get; set; }

        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

    }

}
