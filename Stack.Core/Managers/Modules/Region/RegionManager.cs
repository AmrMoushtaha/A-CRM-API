using Stack.DAL;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.Region;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers.Modules.Materials
{
    public class RegionManager : Repository<Region, ApplicationDbContext>
    {
        public RegionManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }

}
