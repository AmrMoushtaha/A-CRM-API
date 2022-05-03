using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models.Modules.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Auth
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {

        public DbSet<ApplicationUser> dbSet;
        public ApplicationDbContext context;
        public ApplicationUserManager(ApplicationDbContext _context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            dbSet = _context.Set<ApplicationUser>();
            context = _context;

        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await Task.Run(() =>
            {
                var usersResult = dbSet.Where(a => a.Id == userId);
                if (usersResult.ToList().Count != 0)
                {

                    var applicationUserToReturn = usersResult.ToList().FirstOrDefault();

                    return applicationUserToReturn;

                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<List<ApplicationUser>> GetAllSystemUsers()
        {
            return await Task.Run(() =>
            {
                var usersResult = dbSet.Where(a => a.Id != null).ToList();

                return usersResult;

            });
        }

    }

}
