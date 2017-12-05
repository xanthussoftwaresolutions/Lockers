using BusinessLibrary.Model.Locker;
using BusinessLibrary.Model.User;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Repository
{
   public class UserRepository : IUserRepository
    {
        public async Task<List<UserViewModel>> GetAllUser()
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                return await (from a in db.Users
                              select new UserViewModel
                              {
                                  UserId = a.UserId,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  Email = a.Email,
                                  Phone = a.Phone,
                                  LoginUserId=a.LoginUserId,
                                  Pin=a.Pin,
                                  Status=a.Status
                              }).ToListAsync();

            }
        }

        public async Task<bool> SaveUser(UserViewModel model)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                Users User = db.Users.Where(x => x.UserId == model.UserId).FirstOrDefault();
                if (User == null)
                {
                    User = new Users()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        LoginUserId = model.LoginUserId,
                        Pin = model.Pin,
                        Status = model.Status

                    };
                    db.Users.Add(User);

                }
                else
                {
                    User.FirstName = model.FirstName;
                    User.LastName = model.LastName;
                    User.Email = model.Email;
                    User.Phone = model.Phone;
                    User.LoginUserId = model.LoginUserId;
                    User.Pin = model.Pin;
                    User.Status = model.Status;
                }

                return await db.SaveChangesAsync() >= 1;
            }
        }

        public async Task<bool> DeleteUserByID(int id)
        {
            using (LockerDBContext db = new LockerDBContext())
            {

                Users User = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (User != null)
                {
                    db.Users.Remove(User);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }

        public UserViewModel AuthenticateUser(int UserId,int Pin)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                return (from a in db.Users where (a.LoginUserId == UserId && a.Pin==Pin)
                             select new UserViewModel
                             {
                                 UserId = a.UserId,
                                 FirstName = a.FirstName,
                                 LastName = a.LastName,
                                 Email = a.Email,
                                 Phone = a.Phone,
                                 LoginUserId = a.LoginUserId,
                                 Pin = a.Pin,
                                 Status = a.Status
                             }).FirstOrDefault();
            }
        }
        public CheckinDeviceViewModel Checkin(int UserId)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
               var lockerNumber=  (from a in db.Locker
                        where (a.HasDevice == false)
                        select new CheckinDeviceViewModel
                        {
                            LockerNumber = a.LockerId,
                            Status=1
                        }).FirstOrDefault();
              var User = db.Users.Where(x => x.LoginUserId == UserId).FirstOrDefault();
                User.LockerId = lockerNumber.LockerNumber;
                db.SaveChanges();
                return lockerNumber;
            }
           
        }
        public CheckinDeviceViewModel Checkout(int UserId)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                return (from a in db.Users
                        where (a.LoginUserId == UserId)
                        select new CheckinDeviceViewModel
                        {
                            LockerNumber = a.LockerId,
                            Status = 1
                        }).FirstOrDefault();
            }
        }
        public float HasDevice(int UserId)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                float deviceCount = db.Users.Where(x => (x.LoginUserId == UserId && x.LockerId != null)).Count();
                return deviceCount;
            }
        }
    }
}
