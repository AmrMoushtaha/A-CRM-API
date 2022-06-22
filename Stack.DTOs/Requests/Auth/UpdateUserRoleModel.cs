using Stack.DTOs.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Requests.Modules.Auth
{
    public  class UpdateUserRoleModel
    {

       public string UserID { get; set; }

       public string RoleID { get; set; }

    }


}
