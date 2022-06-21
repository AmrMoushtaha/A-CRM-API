
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Stack.DTOs.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.Entities.Models.Modules.Auth
{
    public class ApplicationRole : IdentityRole
    {
       
        public string SystemAuthorizations { get; set; }

        public string NameAR { get; set; }

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public bool HasParent { get; set; }

        public string ParentRoleID { get; set; }

        public AuthorizationsModel GetAuthModel()
        {

            if (SystemAuthorizations != null)
            {

                AuthorizationsModel AuthModel = JsonConvert.DeserializeObject<AuthorizationsModel>(SystemAuthorizations);

                return AuthModel;

            }
            else
            {

                return null;

            }



        }

    }

}
