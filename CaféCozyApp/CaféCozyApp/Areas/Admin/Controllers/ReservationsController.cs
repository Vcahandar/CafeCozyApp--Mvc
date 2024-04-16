using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]


    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly AppDbContext _context;


        public ReservationsController(IReservationService reservationService, AppDbContext context)
        {
            _reservationService = reservationService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await _reservationService.GetAllAsync();

            return View(reservations);
        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return BadRequest();

            Reservation reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null) return NotFound();

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return Ok();
        }
    }
}
