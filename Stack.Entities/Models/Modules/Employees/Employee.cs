using Stack.Entities.Models.Modules.Auth;
using Stack.Entities.Models.Modules.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models.Modules.Employees
{
    public class Employee
    {
        public long ID { get; set; }

        public DateTime? HiringDate { get; set; }

        public DateTime? StartDate { get; set; } 

        public DateTime? EndDate { get; set; }

        public long? ManagerID { get; set; }


        [Column(TypeName = "varchar(255)")]
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Updated_By { get; set; }

        public DateTime Update_Date { get; set; }

        //Navigation Properties . 
        public string ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //Navigation Properties. 
        public int? CostCenterID { get; set; }

        [ForeignKey("CostCenterID")]
        public virtual CostCenter CostCenter { get; set; }

        public virtual List<EmployeeAddress> Addresses { get; set; }
        public virtual List<Employee_PhoneNumber> PhoneNumbers { get; set; }

        public virtual List<Employee_Action> Employee_Actions { get; set; }

        public virtual List<Employee_PurchasingGroup> Employee_PurchasingGroups { get; set; }

        public virtual List<Employee_Position> Employee_Positions { get; set; }

        public virtual List<Employee_SubGroup> Employee_SubGroups { get; set; }


    }

}
