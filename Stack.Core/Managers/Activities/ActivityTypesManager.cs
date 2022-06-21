using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class ActivityTypesManager : Repository<ActivityType, ApplicationDbContext>
    {
        public ActivityTypesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
