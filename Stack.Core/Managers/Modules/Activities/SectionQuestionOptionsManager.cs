using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SectionQuestionOptionsManager : Repository<SectionQuestionOption, ApplicationDbContext>
    {
        public SectionQuestionOptionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
