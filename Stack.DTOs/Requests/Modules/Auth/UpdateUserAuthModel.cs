using Stack.DTOs.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Auth
{
    public  class UpdateUserAuthModel
    {

       public string UserID { get; set; }

       public AuthorizationsModel AuthModel { get; set; }

    }


}
