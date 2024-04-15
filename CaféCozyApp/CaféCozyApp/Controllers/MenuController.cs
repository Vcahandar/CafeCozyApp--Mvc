using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public MenuController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductCategory> categories = await _categoryService.GetAll();
            List<Product> products = await _productService.GetAll();

            MenuVM menuVM = new MenuVM
            {
                
                ProductCategories = categories,
                Products = products
            };

            return View(menuVM);

        }
    }
}
