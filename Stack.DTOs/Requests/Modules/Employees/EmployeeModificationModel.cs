using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EmployeeModificationModel_MasterData
    {
        public long ID { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime StartDate { get; set; }
    } 
    
    public class EmployeeModificationModel_Communication
    {
        public long ID { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Other_Info { get; set; }
    }
}
