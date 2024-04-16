using CaféCozyApp.Areas.Admin.ViewModels.Service;
using CaféCozyApp.Areas.Admin.ViewModels.Slider;
using CaféCozyApp.Data;
using CaféCozyApp.Helpers;
using CaféCozyApp.Models;
using CaféCozyApp.Services;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]


    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAppService _appService;

        public ServicesController(AppDbContext context, IWebHostEnvironment env, IAppService appService)
        {
            _context = context;
            _env = env;
            _appService = appService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Service> services = await _appService.GetAllAsync();
            return View(services);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Create(ServiceCreateVM service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(service);
                }

                if(service.Photo != null)
                {

                    if (!service.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File format should be either png or jpeg only.");
                        return View(service);
                    }

                    if (!service.Photo.CheckFileSize(2097152))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                        return View(service);
                    }

                }
                else
                {
                    ModelState.AddModelError("Image", "Image file is required.");

                }



                string fileName = Guid.NewGuid().ToString() + " " + service.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/services", fileName);
                await FileHelper.SaveFileAsync(newPath, service.Photo);

                Service newService = new()
                {
                    ImageUrl = fileName,
                    Description = service.Description,
                    Title = service.Title,
                    IconUrl = service.IconUrl,

                };

                await _context.Services.AddAsync(newService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Service dbService = await _appService.GetByIdAsync(id);
                if (dbService == null) return NotFound();

                ServiceUpdateVM model = new()
                {
                    ImageUrl = dbService.ImageUrl,
                    Description = dbService.Description,
                    Title = dbService.Title,
                    IconUrl = dbService.IconUrl,
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
        public async Task<IActionResult> Edit(int? id, ServiceUpdateVM service)
        {
            try
            {
                if (id == null) return BadRequest();
                Service dbService = await _appService.GetByIdAsync(id);
                if (dbService == null) return NotFound();

                ServiceUpdateVM model = new()
                {
                    ImageUrl = dbService.ImageUrl,
                    IconUrl = dbService.IconUrl,
                    Title = dbService.Title,
                    Description = dbService.Description,
                };


                if (service.Photo != null)
                {
                    if (!service.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File format should be either png or jpeg only.");
                        return View(model);
                    }
                    if (!service.Photo.CheckFileSize(2097152))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/services", dbService.ImageUrl);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + service.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/services", fileName);
                    await FileHelper.SaveFileAsync(newPath, service.Photo);
                    dbService.ImageUrl = fileName;
                }
                else
                {
                    Service newService = new()
                    {
                        ImageUrl = dbService.ImageUrl
                    };
                }

                dbService.Title = service.Title;
                dbService.IconUrl = service.IconUrl;
                dbService.Description = service.Description;

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
                Service service = await _appService.GetByIdAsync(id);
                if (service == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "uploads/services", service.ImageUrl);
                FileHelper.DeleteFile(path);
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


    }
}
