using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;

namespace CaféCozyApp.Services
{
    public class AboutFeatureService : IAboutFeatureService
    {
        public Task<IEnumerable<AboutFeature>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AboutFeature> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
