using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
