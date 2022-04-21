using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using Stack.Entities.Models.Modules.Auth;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Auth
{

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {


        public DbSet<ApplicationRole> dbSet;
        public ApplicationDbContext context;

        public ApplicationRoleManager(ApplicationDbContext _context, IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<RoleManager<ApplicationRole>> logger) : base(store,roleValidators, keyNormalizer,errors,logger)
        {
            context = _context;
            dbSet = _context.Set<ApplicationRole>();
        }

        public async Task<List<ApplicationRole>> GetAllSystemRoles()
        {
            return await Task.Run(() =>
            {
                var rolesResult = dbSet.Where(a => a.Name != "Administrator").ToList();

                return rolesResult;
              
            });
        }

    }
}
