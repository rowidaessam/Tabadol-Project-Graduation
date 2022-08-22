using Microsoft.AspNetCore.Mvc;

namespace identityWithChristina.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
