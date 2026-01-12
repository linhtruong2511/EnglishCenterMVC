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
        private readonly IAuthService authService;

        public HomeController(ILogger<HomeController> logger,
            IAuthService authService)
        {
            _logger = logger;
            this.authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await authService.Login(email, password);
                if (result.Roles.Contains(Roles.ADMIN.ToString()))
                {
                    return Redirect("/Admin");
                }
                else if (result.Roles.Contains(Roles.STUDENT.ToString()))
                {
                    return Redirect("/Student");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Thông tin ??ng nh?p không chính xác";
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
