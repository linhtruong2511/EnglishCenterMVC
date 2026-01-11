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

        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await submissionService.GetSubmissions();
                return View(list);
            } catch
            {
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Detail(int subId)
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
