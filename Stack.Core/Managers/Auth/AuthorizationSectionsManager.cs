using Stack.DAL;
using Stack.Entities.Models.Modules.Auth;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Auth
{
    public class AuthorizationSectionsManager : Repository<AuthorizationSection, ApplicationDbContext>
    {

        public AuthorizationSectionsManager(ApplicationDbContext _context) : base(_context)
        {



        }

   

    }

}
