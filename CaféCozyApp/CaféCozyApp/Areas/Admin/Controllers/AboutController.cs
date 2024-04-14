using CaféCozyApp.Areas.Admin.ViewModels.AboutFeature;
using CaféCozyApp.Areas.Admin.ViewModels.Slider;
using CaféCozyApp.Data;
using CaféCozyApp.Helpers;
using CaféCozyApp.Models;
using CaféCozyApp.Services;
using CaféCozyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutFeatureService _aboutFeatureService;

        public AboutController(AppDbContext context, 
                                IWebHostEnvironment environment,
                                    IAboutFeatureService aboutFeatureService)
        {
            _context = context;
            _env = environment;
            _aboutFeatureService = aboutFeatureService;
        }

        public async Task<IActionResult> Index()
        {
            List<AboutFeature> aboutFeatures = await _aboutFeatureService.GetAllAsync();

            return View(aboutFeatures);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutFeature dbAboutFeature = await _aboutFeatureService.GetByIdAsync(id);
                if (dbAboutFeature == null) return NotFound();

                AboutFeatureUpdateVM model = new()
                {
                    ImageUrl = dbAboutFeature.ImageUrl,
                    TitleFirst = dbAboutFeature.TitleFirst,
                    TitleSecond = dbAboutFeature.TitleSecond,
                    TitleThird = dbAboutFeature.TitleThird,
                    DescriptionFirst = dbAboutFeature.DescriptionFirst,
                    DescriptionSecond = dbAboutFeature.DescriptionSecond,
                    DescriptionThird = dbAboutFeature.DescriptionThird,
                    AddedTextFirst = dbAboutFeature.AddedTextFirst,
                    AddedTextSecond = dbAboutFeature.AddedTextSecond,
                    AddedTextThird = dbAboutFeature.AddedTextThird,
       
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
        public async Task<IActionResult> Edit(int? id, AboutFeatureUpdateVM featureUpdateVM)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutFeature dbAboutFeature = await _aboutFeatureService.GetByIdAsync(id);
                if (dbAboutFeature == null) return NotFound();

                AboutFeatureUpdateVM model = new()
                {
                    ImageUrl = dbAboutFeature.ImageUrl,
                    TitleFirst = dbAboutFeature.TitleFirst,
                    TitleSecond = dbAboutFeature.TitleSecond,
                    TitleThird = dbAboutFeature.TitleThird,
                    DescriptionFirst = dbAboutFeature.DescriptionThird,
                    DescriptionSecond = dbAboutFeature.DescriptionSecond,
                    DescriptionThird = dbAboutFeature.DescriptionThird,
                    AddedTextFirst = dbAboutFeature.AddedTextFirst,
                    AddedTextSecond = dbAboutFeature.AddedTextSecond,
                    AddedTextThird = dbAboutFeature.AddedTextThird,
                };
                if (featureUpdateVM.Photo != null)
                {
                    if (!featureUpdateVM.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!featureUpdateVM.Photo.CheckFileSize(2097152))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 2 MB");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/about", dbAboutFeature.ImageUrl);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + featureUpdateVM.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "uploads/about", fileName);
                    await FileHelper.SaveFileAsync(newPath, featureUpdateVM.Photo);
                    dbAboutFeature.ImageUrl = fileName;
                }
                else
                {
                    AboutFeature newAboutFeature = new()
                    {
                        ImageUrl = dbAboutFeature.ImageUrl
                    };
                }

                dbAboutFeature.TitleFirst = featureUpdateVM.TitleFirst;
                dbAboutFeature.TitleSecond = featureUpdateVM.TitleSecond;
                dbAboutFeature.TitleThird = featureUpdateVM.TitleThird;
                dbAboutFeature.DescriptionFirst = featureUpdateVM.DescriptionFirst;
                dbAboutFeature.DescriptionSecond = featureUpdateVM.DescriptionSecond;
                dbAboutFeature.DescriptionThird = featureUpdateVM.DescriptionThird;
                dbAboutFeature.AddedTextFirst = featureUpdateVM.AddedTextFirst;
                dbAboutFeature.AddedTextSecond = featureUpdateVM.AddedTextSecond;
                dbAboutFeature.AddedTextThird = featureUpdateVM.AddedTextThird;
     

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
