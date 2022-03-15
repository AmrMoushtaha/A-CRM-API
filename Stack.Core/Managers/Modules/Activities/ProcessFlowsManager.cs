using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class ProcessFlowsManager : Repository<ProcessFlow, ApplicationDbContext>
    {
        public ProcessFlowsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
