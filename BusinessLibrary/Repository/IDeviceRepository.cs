using BusinessLibrary.Model;
using BusinessLibrary.Model.Device;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLibrary
{
    public interface IDeviceRepository
    {
        Task<List<DeviceViewModel>> GetAllDevice();
        Task<bool> SaveDevice(DeviceViewModel model);
        Task<bool> DeleteDeviceByID(int id);
    }
}
