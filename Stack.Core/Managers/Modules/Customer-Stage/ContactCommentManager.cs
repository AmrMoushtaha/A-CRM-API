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
    public class ContactCommentManager : Repository<ContactComment, ApplicationDbContext>
    {
        public DbSet<ContactComment> dbSet;
        public ApplicationDbContext context;

        public ContactCommentManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<ContactComment>();
            context = _context;
        }

    }

}
