using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.Materials
{
    public class ContactTagManager : Repository<Contact_Tag, ApplicationDbContext>
    {
        public DbSet<Contact_Tag> dbSet;
        public ApplicationDbContext context;

        public ContactTagManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Contact_Tag>();
            context = _context;
        }

    }

}
