using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
