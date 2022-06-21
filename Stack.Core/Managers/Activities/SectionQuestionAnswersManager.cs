using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SectionQuestionAnswersManager : Repository<SectionQuestionAnswer, ApplicationDbContext>
    {
        public SectionQuestionAnswersManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
