using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models.Modules.Employees.Positions
{
    public class PositionMainViewDTO
    {

        public int ID { get; set; }

        public string DescriptionAR { get; set; }

        public string DescriptionEN { get; set; }

        public string Code { get; set; }

        public string OrgUnitNameAR { get; set; }

        public string OrgUnitNameEN { get; set; }

        public int OrgUnitID { get; set; }

        public string OrgUnitCode { get; set; }


    }

}
