using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLibrary.Repository;
using BusinessLibrary.Model.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LockerApi.Controllers
{
    [Authorize]
    public class LockerController : Controller
    {
        private IUserRepository userRepository;
        public LockerController(IUserRepository UserRepository) {
            userRepository = UserRepository;
        }
      
        [HttpGet("{id}")]
        [Route("api/locker/hasdevice")]
        public float hasdevice(int id)
        {
            float hasDevice = userRepository.HasDevice(id);
            if (hasDevice == 0) {
                hasDevice = -1;
            }
            return hasDevice;
        }
        [HttpGet("{id}")]
        [Route("api/locker/checkout")]
        public CheckinDeviceViewModel checkout(int id)
        {
            CheckinDeviceViewModel checkOut = userRepository.Checkout(id);
            return checkOut;
        }
        [HttpGet("{id}")]
        [Route("api/locker/checkin")]
        public CheckinDeviceViewModel checkin(int id)
        {
            CheckinDeviceViewModel checkIn = userRepository.Checkin(id);
            return checkIn;
        }

    }
}
