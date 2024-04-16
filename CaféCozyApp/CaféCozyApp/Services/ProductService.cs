using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll() => await _context.Products.Include(m => m.Category).ToListAsync();

        public async Task<Product> GetById(int? id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();


        public async Task<List<Product>> GetPaginatedDatas(int page, int take)
        {
            return await _context.Products
             .Skip((page * take) - take)
             .Take(take)
             .ToListAsync();
        }
    }
}
