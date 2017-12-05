using BusinessLibrary.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLibrary
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContact();
        Task<bool> SaveContact(Contact model);
        Task<bool> DeleteContactByID(int id);
    }
}
