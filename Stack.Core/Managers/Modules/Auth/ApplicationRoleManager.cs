using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using Stack.Entities.Models.Modules.Auth;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Auth
{

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<RoleManager<ApplicationRole>> logger) : base(store,roleValidators, keyNormalizer,errors,logger)
        {


        }

    }
}
