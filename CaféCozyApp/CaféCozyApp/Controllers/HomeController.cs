using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public HomeController(AppDbContext context, ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();

            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
            };

            return View(homeVM);
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