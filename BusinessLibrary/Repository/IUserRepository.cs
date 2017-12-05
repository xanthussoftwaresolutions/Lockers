using BusinessLibrary.Model.Locker;
using BusinessLibrary.Model.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Repository
{
   public interface IUserRepository
    {
        Task<List<UserViewModel>> GetAllUser();
        Task<bool> SaveUser(UserViewModel model);
        Task<bool> DeleteUserByID(int id);
        UserViewModel AuthenticateUser(int UserId, int Pin);
        CheckinDeviceViewModel Checkin(int UserId);
        CheckinDeviceViewModel Checkout(int UserId);
        float HasDevice(int UserId);
    }
}
