using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EmployeeCreation
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? ManagerID { get; set; }

        public virtual EmployeeCreation_ApplicationUser ApplicationUser { get; set; }   
        public virtual List<Address_Creation> Addresses { get; set; }

    }


    public class EmployeeCreation_ApplicationUser
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Gender { get; set; }

    }

    public class Address_Creation
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
        public string BuildingNumber { get; set; }
        public string OtherInfo { get; set; }
        public long? EmplyoeeID { get; set; }
    }
}
