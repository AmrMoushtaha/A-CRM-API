using Stack.DAL;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers.Modules.Materials
{
    public class OpportunityManager : Repository<Opportunity, ApplicationDbContext>
    {
        public OpportunityManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
