using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SelectedOptionsManager : Repository<SelectedOption, ApplicationDbContext>
    {
        public SelectedOptionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
