using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Employees
{
    public class EmployeeMasterDataTableViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public DateTime CreationDate { get; set; }
        public string CompanyCode { get; set; }
    }

}
