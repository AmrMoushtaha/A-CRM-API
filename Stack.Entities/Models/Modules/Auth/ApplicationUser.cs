
using Microsoft.AspNetCore.Identity;
using Stack.Entities.Models.Modules.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.Entities.Models.Modules.Auth
{
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "nvarchar(70)")]
        public string NameAR { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string NameEN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Gender { get; set; }

        //Navigation Properties 
        public virtual Employee Employee { get; set; }

    }

}
