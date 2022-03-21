using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SubmissionDetailsManager : Repository<SubmissionDetails, ApplicationDbContext>
    {
        public SubmissionDetailsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
