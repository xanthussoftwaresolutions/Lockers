using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessLibrary.Model;
using System.Linq;
using BusinessLibrary.Model.Device;

namespace BusinessLibrary
{
    public class DeviceRepository : IDeviceRepository
    {
        public async Task<List<DeviceViewModel>> GetAllDevice()
        {
            try
            {
            using (LockerDBContext db = new LockerDBContext())
            {
                return await (from a in db.Device
                              select new DeviceViewModel
                              {
                                  DeviceId = a.DeviceId,
                                  DeviceName = a.DeviceName,
                                  Status=a.Status,
                                  LockerId=a.LockerId
                              }).ToListAsync();

            }
            }
            catch (Exception ex)
            {
                List<DeviceViewModel> asd = new List<DeviceViewModel>();
                return asd;
            }
        }

        public async Task<bool> SaveDevice(DeviceViewModel model)
        {
            using (LockerDBContext db = new LockerDBContext())
            {
                Device device = db.Device.Where(x => x.DeviceId == model.DeviceId).FirstOrDefault();
                if (device == null)
                {
                    device = new Device()
                    {
                        DeviceName = model.DeviceName,
                        DeviceId=model.DeviceId,
                        Status=model.Status,
                        LockerId=model.LockerId
                    };
                    db.Device.Add(device);

                }
                else
                {
                    device.DeviceName = model.DeviceName;
                    device.DeviceId = model.DeviceId;
                    device.Status = model.Status;
                    device.LockerId = model.LockerId;
                }

                return await db.SaveChangesAsync() >= 1;
            }
        }

        public async Task<bool> DeleteDeviceByID(int id)
        {
            using (LockerDBContext db = new LockerDBContext())
            {

                Device device = db.Device.Where(x => x.Id == id).FirstOrDefault();
                if (device != null)
                {
                    db.Device.Remove(device);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }
    }
}
