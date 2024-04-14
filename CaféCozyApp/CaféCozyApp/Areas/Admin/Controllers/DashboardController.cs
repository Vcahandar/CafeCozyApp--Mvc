using CaféCozyApp.Data;
using CaféCozyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {

        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Message> messages = _context.Messages.ToList();
            return View(messages);
        }
    }
}
