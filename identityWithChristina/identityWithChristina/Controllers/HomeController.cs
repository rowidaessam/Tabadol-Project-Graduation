using identityWithChristina.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using identityWithChristina.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace identityWithChristina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITIContext _context;

        public HomeController(ILogger<HomeController> logger, ITIContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            List<Product> products = new List<Product>();
            products = _context.Products.Where(p => p.ExchangationUserId == null && p.DonationAssId == null).OrderByDescending(p => p.ProductId).Take(6).ToList();
            model.products = products;
            // List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            // applicationUsers = _context.ApplicationUsers.Include(a => a.GeneralFeedbacks).Where(a => (a.GeneralFeedbacks.OrderBy(f => f.Date).LastOrDefault().Rate) > 3).OrderByDescending(a => a.Points).Take(6).ToList();
            //model.applicationUsers=applicationUsers;
            List<GeneralFeedback> generalFeedbacks = new List<GeneralFeedback>();
            generalFeedbacks = _context.GeneralFeedbacks.Include(f => f.User).OrderBy(g => g.Date).Where(g => g.Rate > 3).Take(6).ToList();
            model.generalFeedbacks = generalFeedbacks;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




