using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int? id);
    }
}
