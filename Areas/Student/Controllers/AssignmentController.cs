using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;
using System.Threading.Tasks;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class AssignmentController : Controller
    {
        IAssignmentService assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var assignments = await assignmentService.GetAssignmentsAsync();
                return View(assignments);
            } catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Details(int courseId)
        {
            if (courseId <= 0) return BadRequest();
            try
            {
                var assigned = await assignmentService.GetAssignmentsAsync(courseId);
                return View(assigned);
            } catch
            {
                return NotFound();
            }
        }
    }
}
