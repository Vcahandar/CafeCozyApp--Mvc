using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _environment;

        public SliderController(ISliderService sliderService, IWebHostEnvironment environment)
        {
            _sliderService= sliderService;
            _environment= environment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();

            return View(sliders);
        }


    }
}
