using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductCategory>> GetAll() => await _context.ProductCategories.Include(m => m.Products).ToListAsync();


        public async Task<ProductCategory> GetCategoryById(int? id) => await _context.ProductCategories.FirstOrDefaultAsync(m => m.Id == id);
    }
}
