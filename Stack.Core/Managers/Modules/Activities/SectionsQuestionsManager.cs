using Stack.DAL;
using Stack.Entities.Models.Modules.Activities;
using Stack.Repository;

namespace Stack.Core.Managers.Modules.Activities
{
    public class SectionsQuestionsManager : Repository<SectionQuestion, ApplicationDbContext>
    {
        public SectionsQuestionsManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
