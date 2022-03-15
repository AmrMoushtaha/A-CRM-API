using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Employees
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public DateTime CreationDate { get; set; }
        
    }

}
