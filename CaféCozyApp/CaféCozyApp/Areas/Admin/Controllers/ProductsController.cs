using CaféCozyApp.Areas.Admin.ViewModels.Product;
using CaféCozyApp.Areas.Admin.ViewModels.Slider;
using CaféCozyApp.Data;
using CaféCozyApp.Helpers;
using CaféCozyApp.Models;
using CaféCozyApp.Services;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(AppDbContext context, IWebHostEnvironment environment, IProductService productService, ICategoryService categoryService)
        {
            _context = context;
            _env = environment;
            _productService = productService;
            _categoryService = categoryService;
        }



        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatas(page, take);
            List<ProductListVM> mappedDatas = GetMappedDatas(datas);
            int pageCount = await GetPageCountAsync(take);
            ViewBag.take = take;
            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Description = product.Description,
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.ProductCategories = await GetCategoryAsync();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {
                ViewBag.ProductCategories = await GetCategoryAsync();

                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(model);
                }

                if (!model.Photo.CheckFileSize(2097152))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                    return View(model);

                }

                string fileName = Guid.NewGuid().ToString() + " " + model.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/products", fileName);
                await FileHelper.SaveFileAsync(newPath, model.Photo);

                Product newProduct = new()
                {
                    ImageUrl = fileName,
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                };


                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.ProductCategories = await GetCategoryAsync();


                if (id == null) return BadRequest();
                Product dbProduct = await _productService.GetById(id);
                if (dbProduct == null) return NotFound();

                ProductUpdateVM model = new()
                {
                    ImageUrl = dbProduct.ImageUrl,
                    Description = dbProduct.Description,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    CategoryId = dbProduct.CategoryId,

                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM updatedProduct)
        {
            try
            {

                ViewBag.ProductCategories = await GetCategoryAsync();

                if (id == null) return BadRequest();
                Product dbProduct = await _productService.GetById(id);
                if (dbProduct == null) return NotFound();

                ProductUpdateVM model = new()
                {
                    ImageUrl = dbProduct.ImageUrl,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    CategoryId = dbProduct.CategoryId
                };
                if (updatedProduct.Photo != null)
                {
                    if (!updatedProduct.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!updatedProduct.Photo.CheckFileSize(2097152))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/products", dbProduct.ImageUrl);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + updatedProduct.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/products", fileName);
                    await FileHelper.SaveFileAsync(newPath, updatedProduct.Photo);
                    dbProduct.ImageUrl = fileName;
                }
                else
                {
                    Product newProduct = new()
                    {
                        ImageUrl = dbProduct.ImageUrl
                    };
                }

                dbProduct.Name = updatedProduct.Name;
                dbProduct.Description = updatedProduct.Description;
                dbProduct.Price = updatedProduct.Price;
                dbProduct.CategoryId = updatedProduct.CategoryId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Product product = await _productService.GetById(id);
                if (product == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "uploads/products", product.ImageUrl);
                FileHelper.DeleteFile(path);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        private async Task<SelectList> GetCategoryAsync()
        {
            List<ProductCategory> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }



    }
}
