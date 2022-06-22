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
    public class ContactPhoneNumberManager : Repository<ContactPhoneNumber, ApplicationDbContext>
    {
        public DbSet<ContactPhoneNumber> dbSet;
        public ApplicationDbContext context;
        public ContactPhoneNumberManager(ApplicationDbContext _context) : base(_context)
        {
            dbSet = _context.Set<ContactPhoneNumber>();
            context = _context;

        }

        //public async Task<ContactViewModel> GetContactDetails(long contactID)
        //{

        //    return await Task.Run(() =>
        //    {
        //        var Contact = context.Contacts.Where(t => t.ID == contactID)
        //           .Select(p => new ContactViewModel
        //           {
        //               ID = p.ID,
        //               FullNameAR = p.FullNameAR,
        //               FullNameEN = p.FullNameEN,
        //               Address = p.Address,
        //               Email = p.Email,
        //               LeadSourceName = p.LeadSourceName,
        //               LeadSourceType = p.LeadSourceType,
        //               Occupation = p.Occupation,
        //               PrimaryPhoneNumber = p.PrimaryPhoneNumber,
        //               Status = p.Status.Status,
        //               AssignedUserName = p.AssignedUser.FirstName + " " + p.AssignedUser.LastName,
        //           }).FirstOrDefault();

        //        if (Contact != null)
        //        {
        //            var phoneNumbers = context.ContactPhoneNumbers.Where(t => t.ContactID == contactID)
        //            .Select(t => new ContactPhoneNumberDTO
        //            {
        //                ID = t.ID,
        //                Number = t.Number,
        //            }).ToList();

        //            if (phoneNumbers != null)
        //            {
        //                Contact.ContactPhoneNumbers = phoneNumbers;
        //            }

        //            return Contact;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    });
        //}
    }

}
