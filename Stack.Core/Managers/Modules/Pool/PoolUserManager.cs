using Microsoft.EntityFrameworkCore;
using Stack.DAL;
using Stack.DTOs.Models.Modules.Pool;
using Stack.Entities.Enums.Modules.CustomerStage;
using Stack.Entities.Models.Modules.CustomerStage;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers.Modules.pool
{
    public class PoolUserManager : Repository<Pool_Users, ApplicationDbContext>
    {


        public DbSet<Pool_Users> dbSet;
        public ApplicationDbContext context;
        public PoolUserManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Pool_Users>();
            context = _context;

        }


        public async Task<List<PoolSidebarViewModel>> GetUserPools(string userID)
        {

            return await Task.Run(() =>
            {
                return context.Pool_Users.Where(t => t.UserID == userID)
                    .Select(t => t.Pool)
                       .Select(p => new PoolSidebarViewModel
                       {
                           ID = p.ID,
                           NameEN = p.NameEN,
                           NameAR = p.NameAR,
                           ContactCount = p.Contacts.Where(t => t.Status.Status == (int)CustomerStageState.Unassigned).Count()
                       }).ToList();
            });
        }

        public async Task<List<ContactListViewModel>> GetPoolContacts(long poolID, string userID)
        {
            return await Task.Run(() =>
            {

                var enumerable = context.Pool_Users.Where(t => t.PoolID == poolID && t.UserID == userID)
                            .Select(a => new
                            {
                                ID = a.PoolID,
                                filteredContacts = a.Pool.Contacts.Where(t => t.Status.Status == (int)CustomerStageState.Unassigned)
                            })
                            .Select(a => a.
                            filteredContacts.Select(p => new ContactListViewModel
                            {
                                ID = p.ID,
                                FullNameEN = p.FullNameEN,
                                FullNameAR = p.FullNameAR,
                                PrimaryPhoneNumber = p.PrimaryPhoneNumber
                            }));

                List<ContactListViewModel> asList = new List<ContactListViewModel>();

                foreach (var item in enumerable)
                {
                    asList.Add(item.FirstOrDefault());
                };

                var contacts = asList;
                if (contacts != null && contacts.Count > 0)
                {
                    return contacts;
                }
                else
                {
                    return null;
                }
            });
        }
    }

}
