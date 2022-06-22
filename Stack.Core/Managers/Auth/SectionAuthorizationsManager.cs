using Stack.DAL;
using Stack.Entities.Models.Modules.Auth;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Auth
{
    public class SectionAuthorizationsManager : Repository<SectionAuthorization, ApplicationDbContext>
    {

        public SectionAuthorizationsManager(ApplicationDbContext _context) : base(_context)
        {



        }

   

    }

}
