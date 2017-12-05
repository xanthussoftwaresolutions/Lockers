using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLibrary;
using BusinessLibrary.Model.Device;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLocker.Controllers
{
    public class DeviceController : Controller
    {
        public IDeviceRepository deviceRepo;
        public DeviceController(IDeviceRepository DeviceRepo)
        {
            deviceRepo = DeviceRepo;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetDevices()
        {
            var data = await deviceRepo.GetAllDevice();
            return Json(new { result = data });
        }

        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> SaveDevice([FromBody] DeviceViewModel model)
        {
            return Json(await deviceRepo.SaveDevice(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeviceByID(int id)
        {
            return Json(await deviceRepo.DeleteDeviceByID(id));
        }
    }
}
