using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly AppDbContext _context;
        public SubscriberService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subscriber>> GetAllAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }

        public async Task<Subscriber> GetByIdAsync(int? id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
