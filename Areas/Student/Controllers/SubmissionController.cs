using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class SubmissionController : Controller
    {
        ISubmissionService submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
