using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;
using System.Threading.Tasks;

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
        
        public IActionResult Detail(int subId)
        {
            if(subId <= 0 ) return BadRequest("Submission khong ton tai");
            try
            {
                var submission = submissionService.GetSubmission(subId);
                return View(submission);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
