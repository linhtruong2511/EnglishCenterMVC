using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Authorization;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class SubmissionController : Controller
    {
        ISubmissionService submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await submissionService.GetSubmissions();
                return View(list);
            }
            catch (Exception ex)
            {
                return Content("Submission Index crashed: " + ex.Message);
            }
        }


        public async Task<IActionResult> Details(int subId)
        {
            if(subId <= 0 ) return BadRequest("Submission khong ton tai");
            try
            {
                var submission = await submissionService.GetSubmission(subId);
                return View(submission);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
