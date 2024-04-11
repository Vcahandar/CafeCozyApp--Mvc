using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
    }
}
