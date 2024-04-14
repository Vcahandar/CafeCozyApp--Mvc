using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface IAboutFeatureService
    {
        Task<List<AboutFeature>> GetAllAsync();
        Task<AboutFeature> GetByIdAsync(int? id);
    }
}
