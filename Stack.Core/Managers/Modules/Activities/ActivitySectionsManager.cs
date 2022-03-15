using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;


namespace Stack.Core.Managers.Modules.Activities
{
    public class ActivitySectionsManager : Repository<ActivitySection, ApplicationDbContext>
    {
        public ActivitySectionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
