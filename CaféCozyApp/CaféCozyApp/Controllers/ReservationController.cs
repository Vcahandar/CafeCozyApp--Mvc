using Microsoft.AspNetCore.Mvc;

namespace CaféCozyApp.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
