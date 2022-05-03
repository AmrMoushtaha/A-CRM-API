using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Materials
{
    public class LeadSourceTypeManager : Repository<LeadSourceType, ApplicationDbContext>
    {

        public LeadSourceTypeManager(ApplicationDbContext _context) : base(_context)
        {
        }

    }

}
