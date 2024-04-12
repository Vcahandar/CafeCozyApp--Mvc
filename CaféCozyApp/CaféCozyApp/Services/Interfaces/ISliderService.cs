using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int? id);
    }
}
