using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Organizations.CompanyCodes
{
    public class EditCompanyCodeModel
    {

        public int ID { get; set; }
        public string Code { get; set; }

        public string Currency { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

    }

}
