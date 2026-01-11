using EnglishCenterMVC.Areas.Student.Models;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

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
                var newlyUploaded = availableAssignments.Where(a => (DateTime.Now - a.CreateAt).TotalDays <= 3).Count();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFile(int assignmentId, IFormFile submissionFile)
        {
            if (assignmentId <= 0) return BadRequest("Invalid assignmentId.");
            if (submissionFile == null || submissionFile.Length == 0) return BadRequest("File không tồn tại");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("User not logged in / missing NameIdentifier.");

            try
            {
                await submissionService.SubmitAssignment(assignmentId, "HV001", submissionFile);
                return RedirectToAction("Index", "Submission", new { area = "Student" });
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest("DB error: " + (dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Exception ex)
            {
                return BadRequest("Gửi bài thất bại: " + ex.Message);
            }
        }
    }
}
