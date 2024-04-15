using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int? id);


    }
}
