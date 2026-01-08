using EnglishCenter.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
