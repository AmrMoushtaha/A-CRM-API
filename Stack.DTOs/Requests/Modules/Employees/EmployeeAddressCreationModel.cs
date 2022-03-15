using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EmployeeAddressCreationModel
    {
        public long ID { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Other_Info { get; set; }


    }
}
