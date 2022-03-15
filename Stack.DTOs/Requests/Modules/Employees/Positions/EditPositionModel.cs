using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Employees
{

    public class EditPositionModel
    {
        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string Code { get; set; }

        public int PositionID { get; set; }

        public int OrgUnitID { get; set; }

    }

}
