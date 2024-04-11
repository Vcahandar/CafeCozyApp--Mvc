using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface IAboutFeatureService
    {
        Task<IEnumerable<AboutFeature>> GetAllAsync();
        Task<AboutFeature> GetByIdAsync(int? id);
    }
}
