using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;


namespace Stack.Core.Managers.Modules.Activities
{
    public class ActivitiesManager : Repository<Activity, ApplicationDbContext>
    {
        public ActivitiesManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
