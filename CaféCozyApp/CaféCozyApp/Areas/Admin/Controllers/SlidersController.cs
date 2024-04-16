using CaféCozyApp.Areas.Admin.ViewModels.Slider;
using CaféCozyApp.Data;
using CaféCozyApp.Helpers;
using CaféCozyApp.Models;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class SlidersController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public SlidersController(ISliderService sliderService, 
                                IWebHostEnvironment environment,
                                AppDbContext context)
        {
            _sliderService= sliderService;
            _env = environment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllAsync();

            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(slider);
                }

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(slider);
                }

                if (!slider.Photo.CheckFileSize(2097152))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                    return View(slider);

                }

                string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/sliders", fileName);
                await FileHelper.SaveFileAsync(newPath, slider.Photo);

                Slider newSlider = new()
                {
                    ImageUrl = fileName,
                    Description = slider.Description,
                    Title = slider.Title,
                    Subtitle = slider.Subtitle,

                };

                await _context.Sliders.AddAsync(newSlider);
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
                Slider dbSlider = await _sliderService.GetByIdAsync(id);
                if (dbSlider == null) return NotFound();

                SliderUpdateVM model = new()
                {
                    ImageUrl = dbSlider.ImageUrl,
                    Description = dbSlider.Description,
                    Title = dbSlider.Title,
                    Subtitle = dbSlider.Subtitle,
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
        public async Task<IActionResult> Edit(int? id, SliderUpdateVM slider)
        {
            try
            {
                if (id == null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync(id);
                if (dbSlider == null) return NotFound();

                SliderUpdateVM model = new()
                {
                    ImageUrl = dbSlider.ImageUrl,
                    Subtitle = dbSlider.Subtitle,
                    Title = dbSlider.Title,
                    Description = dbSlider.Description,
                };
                if (slider.Photo != null)
                {
                    if (!slider.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!slider.Photo.CheckFileSize(2097152))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/sliders", dbSlider.ImageUrl);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/sliders", fileName);
                    await FileHelper.SaveFileAsync(newPath, slider.Photo);
                    dbSlider.ImageUrl = fileName;
                }
                else
                {
                    Slider newSlider = new()
                    {
                        ImageUrl = dbSlider.ImageUrl
                    };
                }

                dbSlider.Title = slider.Title;
                dbSlider.Subtitle = slider.Subtitle;
                dbSlider.Description = slider.Description;

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
                Slider slider = await _sliderService.GetByIdAsync(id);
                if (slider == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
                FileHelper.DeleteFile(path);
                _context.Sliders.Remove(slider);
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
