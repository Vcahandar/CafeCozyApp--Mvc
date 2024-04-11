using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
