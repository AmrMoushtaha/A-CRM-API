
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.DTOs.Models.Modules.Auth
{
    public class SectionAuthorizationModel
    {
        public long ID { get; set; }

        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Code { get; set; }

        public bool IsAuthorized { get; set; }

        public long AuthorizationSectionID { get; set; }

    }

}
