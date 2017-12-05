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
   public class LockerRepository : ILockerRepository
    {
        public async Task<List<LockerViewModel>> GetAllLocker()
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                return await (from a in db.Locker
                              select new LockerViewModel
                              {
                                  CabinetId = a.CabinetId,
                                  Id=a.Id,
                                  DeviceId=a.DeviceId,
                                  HasDevice=a.HasDevice,
                                  LockerId=a.LockerId,
                                  Name=a.Name
                              }).ToListAsync();

            }
        }
    }
}
