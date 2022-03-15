using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EmployeeCreationModel
    {
        public EmployeeCreation_MasterData MasterData { get; set; }
        public EmployeeCreation_Communication Communication { get; set; }
        public EmployeeCreation_CompanyCode CompanyCode { get; set; }

    }


    public class EmployeeCreation_MasterData
    {
        public string UserName { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class EmployeeCreation_Communication
    {
        public List<EmployeeCreation_PhoneNumber> PhoneNumbers { get; set; }
        public List<EmployeeCreation_Address> Addresses { get; set; }
    }

    public class EmployeeCreation_CompanyCode
    {
        public long CodeID { get; set; }
        public List<EmployeeCreation_OrgUnits> OrgUnits { get; set; }

    }



    public class EmployeeCreation_PhoneNumber
    {
        public string Number { get; set; }
    }

    public class EmployeeCreation_Address
    {
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Building_Number { get; set; }
        public string Other_Info { get; set; }

    }

    public class EmployeeCreation_OrgUnits
    {
        public long OrgUnitId { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public double Percentage { get; set; }
    }
}
