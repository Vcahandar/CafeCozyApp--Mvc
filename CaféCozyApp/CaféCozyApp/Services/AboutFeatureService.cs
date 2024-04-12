using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class AboutFeatureService : IAboutFeatureService
    {
        private readonly AppDbContext _context;
        public AboutFeatureService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AboutFeature>> GetAllAsync()
        {
            return await _context.AboutFeatures.ToListAsync();
        }

        public async Task<AboutFeature> GetByIdAsync(int? id)
        {
            return await _context.AboutFeatures.FirstOrDefaultAsync(b => b.Id == id);

        }
    }
}
