using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Services
{
    public class ContactService : IContactService
    {

        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
             _context = context;
        }


        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(b => b.Id == id);

        }
    }
}
