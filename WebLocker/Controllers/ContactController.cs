using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLibrary.Model;
using BusinessLibrary;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLocker.Controllers
{
    public class ContactController : Controller
    {
        public IContactRepository ContactRepo;

        public ContactController(IContactRepository contactRepo)
        {
            ContactRepo = contactRepo;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetContacts()
        {
            var data = await ContactRepo.GetAllContact();
            return Json(new { result = data });
        }

        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> SaveContact([FromBody] Contact model)
        {
            return Json(await ContactRepo.SaveContact(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContactByID(int id)
        {
            return Json(await ContactRepo.DeleteContactByID(id));
        }
    }
}
