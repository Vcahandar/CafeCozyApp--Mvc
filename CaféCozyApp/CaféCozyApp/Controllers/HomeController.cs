using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService; 
        }

        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _sliderService.GetAllAsync();

            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
    
            };


            return View(homeVM);
        }

    }
}