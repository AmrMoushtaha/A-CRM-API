
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.DTOs.Models.Modules.Auth
{
    public class ApplicationUserDTO 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

}
