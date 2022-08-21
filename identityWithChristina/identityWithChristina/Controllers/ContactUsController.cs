using Microsoft.AspNetCore.Mvc;

namespace identityWithChristina.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
