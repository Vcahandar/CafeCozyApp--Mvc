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
        private readonly IAppService _appService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;


        public HomeController(AppDbContext context, 
                                ISliderService sliderService,
                                IAppService appService, 
                                ICategoryService categoryService,
                                IProductService productService)
        {
            _context = context;
            _sliderService = sliderService;
            _appService = appService;
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();
            IEnumerable<Service> services = await _appService.GetAllAsync();
            List<ProductCategory> categories = await _categoryService.GetAll();
            List<Product> products = await _productService.GetAll();


            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Services = services,
                ProductCategories = categories,
                Products = products
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