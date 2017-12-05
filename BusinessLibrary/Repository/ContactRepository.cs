using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessLibrary.Model;
using System.Linq;

namespace BusinessLibrary
{
    public class ContactRepository : IContactRepository
    {
        public async Task<List<Contact>> GetAllContact()
        {
            try
            {
            using (LockerDBContext db = new LockerDBContext())
            {
                return await (from a in db.Contacts
                              select new Contact
                              {
                                  ContactId = a.ContactId,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  Email = a.Email,
                                  Phone = a.Phone
                              }).ToListAsync();

            }
            }
            catch (Exception ex)
            {
                List<Contact> asd = new List<Contact>();
                return asd;
            }
        }

        public async Task<bool> SaveContact(Contact model)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                Contacts contact = db.Contacts.Where(x => x.ContactId == model.ContactId).FirstOrDefault();
                if (contact == null)
                {
                    contact = new Contacts()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone
                    };
                    db.Contacts.Add(contact);

                }
                else
                {
                    contact.FirstName = model.FirstName;
                    contact.LastName = model.LastName;
                    contact.Email = model.Email;
                    contact.Phone = model.Phone;
                }

                return await db.SaveChangesAsync() >= 1;
            }
        }

        public async Task<bool> DeleteContactByID(int id)
        {
            using (LockerDBContext db = new LockerDBContext())
            {

                Contacts contact = db.Contacts.Where(x => x.ContactId == id).FirstOrDefault();
                if (contact != null)
                {
                    db.Contacts.Remove(contact);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }
    }
}
