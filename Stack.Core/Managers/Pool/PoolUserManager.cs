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

namespace Stack.Core.Managers.Modules.Pools
{
    public class PoolUserManager : Repository<Pool_User, ApplicationDbContext>
    {


        public DbSet<Pool_User> dbSet;
        public ApplicationDbContext context;
        public PoolUserManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<Pool_User>();
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
                       }).ToList();
            });
        }

        public async Task<ContactListMenuView> GetPoolContacts(long poolID, string userID, int pageNumber)
        {

            return await Task.Run(() =>
            {
                var enumerable = context.Pool_Users.Where(t => t.PoolID == poolID && t.UserID == userID)
                            .Select(a => new
                            {
                                ID = a.PoolID,
                                filteredContacts = a.Pool.Contacts.Where(t => t.State == (int)CustomerStageState.Unassigned)
                            })
                            .SelectMany(a => a.
                            filteredContacts.Select(p => new ContactListViewModel
                            {
                                ID = p.ID,
                                FullNameAR = p.FullNameAR,
                                FullNameEN = p.FullNameEN,
                                PrimaryPhoneNumber = p.PrimaryPhoneNumber,
                                IsLocked = p.IsLocked
                            }));

                var totalRecordsCount = enumerable.Count();
                var paginatedRecords = enumerable.Skip((pageNumber - 1) * 10).Take(10).ToList();

                var contacts = paginatedRecords.ToList();
                if (contacts != null && contacts.Count > 0)
                {
                    ContactListMenuView response = new ContactListMenuView
                    {
                        TotalRecords = totalRecordsCount,
                        Records = contacts
                    };

                    return response;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<List<ContactListViewModel>> GetPoolFreshContacts(long poolID, string userID)
        {

            return await Task.Run(() =>
            {
                var enumerable = context.Pool_Users.Where(t => t.PoolID == poolID && t.UserID == userID)
                            .Select(a => new
                            {
                                ID = a.PoolID,
                                filteredContacts = a.Pool.Contacts.Where(t => t.State == (int)CustomerStageState.Unassigned && t.IsFresh == true)
                            })
                            .SelectMany(a => a.
                            filteredContacts.Select(p => new ContactListViewModel
                            {
                                ID = p.ID,
                                FullNameAR = p.FullNameAR,
                                FullNameEN = p.FullNameEN,
                                PrimaryPhoneNumber = p.PrimaryPhoneNumber,
                                IsLocked = p.IsLocked
                            })).ToList();

                List<ContactListViewModel> asList = new List<ContactListViewModel>();

                foreach (var item in enumerable)
                {

                    asList.Add(item);
                };

                var contacts = enumerable.ToList();
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
