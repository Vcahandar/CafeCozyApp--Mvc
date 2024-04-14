using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Controllers
{
    public class AboutController : Controller
    {

        private readonly IAboutFeatureService _aboutFeatureService;

        public AboutController(IAboutFeatureService aboutFeatureService)
        {
            _aboutFeatureService = aboutFeatureService;
        }



        public async Task<IActionResult> Index()
        {

            List<AboutFeature> aboutFeatures = await _aboutFeatureService.GetAllAsync();

            AboutVM aboutVM = new AboutVM
            {
                AboutFeatures = aboutFeatures
            };


            return View(aboutVM);
        }
    }
}
