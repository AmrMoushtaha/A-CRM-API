using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.System
{
    public  class AuthorizationSection
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public string Code { get; set; }

        public List<SectionAuthorization> SectionAuthorizations { get; set; }
    }

}
