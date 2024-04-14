using CaféCozyApp.Data;
using CaféCozyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Reservations.AddAsync(reservation);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
