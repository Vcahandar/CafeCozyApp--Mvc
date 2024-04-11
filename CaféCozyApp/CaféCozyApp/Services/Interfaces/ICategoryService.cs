using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<ProductCategory>> GetAll();
        Task<ProductCategory> GetCategoryById(int? id);
    }
}
