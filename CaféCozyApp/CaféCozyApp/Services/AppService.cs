using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class AppService: IAppService
    {
        private readonly AppDbContext _context;
        public AppService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int? id)
        {
            return await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
