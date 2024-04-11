using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class ReservationService : IReservationService
    {

        private readonly AppDbContext _context;
        public ReservationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int? id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
