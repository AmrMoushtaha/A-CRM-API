using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Auth
{
    public class CreateDummyUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public string Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

}
