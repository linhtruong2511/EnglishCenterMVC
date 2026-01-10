using System.Threading.Tasks;
using EnglishCenterMVC.Areas.Student.Models;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class AssignmentController : Controller
    {
        IAssignmentService assignmentService;
        ISubmissionService submissionService;

        public AssignmentController(IAssignmentService assignmentService, ISubmissionService submissionService)
        {
            this.assignmentService = assignmentService;
            this.submissionService = submissionService;
        }
        public async Task<IActionResult> Index(int courseId)
        {
            try
            {
                var availableAssignments = await assignmentService.GetAssignmentsAsync();
                var overdueAssignments = await assignmentService.GetOverdueAssignmentsAsync();
                var newlyUploaded = availableAssignments.Where(a => (a.CreateAt - DateTime.Now).TotalDays <= 3).Count();
                return View(new AssignmentVM
                {
                    Available = availableAssignments.Count(),
                    AvailableAssignments = availableAssignments,
                    OverdueAssignments = overdueAssignments,
                    NewlyUploaded = newlyUploaded
                });
            } catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0) return BadRequest();
            try
            {
                var content = await assignmentService.GetAssignmentAsync(id);
                return View(content);
            } catch
            {
                return NotFound();
            }
        }
    }
}
