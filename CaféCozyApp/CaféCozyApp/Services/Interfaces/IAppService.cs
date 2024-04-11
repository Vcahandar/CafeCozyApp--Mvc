using CaféCozyApp.Models;


namespace CaféCozyApp.Services.Interfaces
{
    public interface IAppService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(int? id);
    }
}
