using Microsoft.AspNetCore.Mvc;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    public class SubmissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
