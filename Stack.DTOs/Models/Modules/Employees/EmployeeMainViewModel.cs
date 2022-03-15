using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Employees
{
    public class EmployeeMainViewModel
    {
        public long ID { get; set; }
        public Employee_MasterData MasterData { get; set; }
        public Employee_Communication Communication { get; set; }
        public Employee_CompanyCode CompanyCode { get; set; }
    }

    public class Employee_MasterData
    {
        public string? UserName { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? StartDate { get; set; }
    }

    public class Employee_Communication
    {
        public List<Employee_PhoneNumberDTO> PhoneNumbers { get; set; }
        public List<Employee_Address> Addresses { get; set; }
    }

    public class Employee_CompanyCode
    {
        public long CodeID { get; set; }
        public string Code { get; set; }
        public List<Employee_OrgUnits>? OrgUnits { get; set; }

    }

    public class Employee_Address
    {
        public long ID { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string OtherInfo { get; set; }
    }

    public class Employee_OrgUnits
    {
        public string OrgUnitDescriptionEN { get; set; }
        public string OrgUnitDescriptionAR { get; set; }
        public string PositionDescriptionEN { get; set; }
        public string PositionDescriptionAR { get; set; }
        public DateTime? StartDate { get; set; }
        public double Percentage { get; set; }
        public long OrgUnitId { get; set; }
        public int PositionId { get; set; }
    }
}
