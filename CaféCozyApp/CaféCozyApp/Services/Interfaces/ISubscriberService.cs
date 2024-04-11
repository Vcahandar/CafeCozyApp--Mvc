using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<IEnumerable<Subscriber>> GetAllAsync();
        Task<Subscriber> GetByIdAsync(int? id);
    }
}
