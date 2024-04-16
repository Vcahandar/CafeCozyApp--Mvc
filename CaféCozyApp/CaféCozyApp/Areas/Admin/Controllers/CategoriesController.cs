using CaféCozyApp.Areas.Admin.ViewModels.Category;
using CaféCozyApp.Areas.Admin.ViewModels.Slider;
using CaféCozyApp.Data;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]


    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoriesController(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductCategory> category = await _categoryService.GetAll();

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            try
            {
                ProductCategory newCategory = new()
                {
                    Name = category.Name
                };

                await _context.ProductCategories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            ProductCategory dbCategory = await _categoryService.GetCategoryById(id);
            if (dbCategory is null) return NotFound();

            CategoryUpdateVM model = new()
            {
                Name = dbCategory.Name
            };
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryUpdateVM categoryUpdate)
        {
            try
            {
                if (id == null) return BadRequest();
                ProductCategory dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory is null) return NotFound();

                CategoryUpdateVM model = new()
                {
                    Name = dbCategory.Name
                };
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                dbCategory.Name = categoryUpdate.Name;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return BadRequest();

            ProductCategory productCategory = await _categoryService.GetCategoryById(id);
            if (productCategory == null) return NotFound();

            _context.ProductCategories.Remove(productCategory);
            _context.SaveChanges();

            return Ok();
        }


    }
}

