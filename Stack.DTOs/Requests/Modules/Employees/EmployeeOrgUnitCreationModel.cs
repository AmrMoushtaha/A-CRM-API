using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EmployeeOrgUnitCreationModel
    {
        public long EmployeeID { get; set; }
        public long OrgUnitID { get; set; }
        public int PositionID { get; set; }
        public DateTime StartDate { get; set; }
        public double Percentage { get; set; }

    }
}
