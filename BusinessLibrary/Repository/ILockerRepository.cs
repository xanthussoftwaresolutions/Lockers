using BusinessLibrary.Model.Locker;
using BusinessLibrary.Model.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Repository
{
   public interface ILockerRepository
    {
        Task<List<LockerViewModel>> GetAllLocker();
    }
}
