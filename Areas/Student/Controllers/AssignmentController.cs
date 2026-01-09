using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;
using System.Threading.Tasks;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class AssignmentController : Controller
    {
        IAssignmentService assignmentService;
        ICourseService courseService;

        public AssignmentController(IAssignmentService assignmentService, ICourseService courseService)
        {
            this.assignmentService = assignmentService;
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index(int courseId)
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
