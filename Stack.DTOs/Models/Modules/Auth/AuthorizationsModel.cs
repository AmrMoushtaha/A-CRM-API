
using System;
using System.Collections.Generic;


namespace Stack.DTOs.Models.Modules.Auth
{
    public class AuthorizationsModel
    {
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public List<AuthorizationSectionModel> AuthorizationSections { get; set; }

    }

}
