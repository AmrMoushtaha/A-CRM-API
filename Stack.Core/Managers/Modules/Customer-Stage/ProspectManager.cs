using Microsoft.EntityFrameworkCore;
using Stack.DAL;
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
    public class ProspectManager : Repository<Prospect, ApplicationDbContext>
    {
        public DbSet<Prospect> dbSet;
        public DbSet<Prospect> phoneNumberdbSet;
        public ApplicationDbContext context;

        public ProspectManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Prospect>();
            context = _context;
        }


        public async Task<List<ContactListViewModel>> GetPoolProspects(string userID, long poolID)
        {
            return await Task.Run(() =>
            {
                return context.Prospects.Where(t => t.Deal.PoolID == poolID)
                   .Select(p => new ContactListViewModel
                   {
                       ID = p.ID,
                       FullNameEN = p.AssignedUserID,
                       FullNameAR = p.AssignedUserID,
                       PrimaryPhoneNumber = p.AssignedUserID,
                   }).ToList();

            });
        }
    }

}
