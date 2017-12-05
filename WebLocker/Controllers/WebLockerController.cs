using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLibrary.Model.User;
using BusinessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLocker.Controllers
{
    [Authorize]
    public class WebLockerController : Controller
    {

        public IUserRepository UserRepo;
        public ILockerRepository LockerRepo;
        public WebLockerController(IUserRepository userRepo, ILockerRepository lockerRepo)
        {
            UserRepo = userRepo;
            LockerRepo = lockerRepo;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            var data = await UserRepo.GetAllUser();
            return Json(new { result = data });
        }

        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> SaveUser([FromBody] UserViewModel model)
        {
            return Json(await UserRepo.SaveUser(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserByID(int id)
        {
            return Json(await UserRepo.DeleteUserByID(id));
        }
        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetLockers()
        {
            var data = await LockerRepo.GetAllLocker();
            return Json(new { result = data });
        }
    }
}
