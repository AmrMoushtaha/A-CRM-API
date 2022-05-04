
using Microsoft.AspNetCore.Identity;
using Stack.Entities.Models.Modules.Common;
using Stack.Entities.Models.Modules.CustomerStage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.Entities.Models.Modules.Auth
{
    public class SectionAuthorization
    {

        public long ID { get; set; }

        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Code { get; set; }


        public long AuthorizationSectionID { get; set; }

        [ForeignKey("AuthorizationSectionID")]
        public virtual AuthorizationSection AuthorizationSection { get; set; }


    }

}
