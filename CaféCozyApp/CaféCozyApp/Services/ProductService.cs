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
    }
}
