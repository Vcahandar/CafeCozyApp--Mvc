using CaféCozyApp.Data;
using CaféCozyApp.Services.Interfaces;

namespace CaféCozyApp.Services
{
    public class LayoutService : ILayoutService
    {
        private AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetSettingsData()
        {
            return _context.Settings.ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
