using System.Diagnostics;
using System.Threading.Tasks;
using EnglishCenterMVC.Authorization;
using EnglishCenterMVC.Models;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnglishCenterMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger,
            IAuthService authService)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
