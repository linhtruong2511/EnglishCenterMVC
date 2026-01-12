using System.Security.Claims;
using System.Threading.Tasks;
using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Areas.Student.Models;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var now = DateTime.Now;

            // 1. Courses
            var courses = await _context.Classes
                .Where(c => c.Users.Any(u => u.Id == userId))
                .Select(c => c.Course)
                .Distinct()
                .ToListAsync();

            // 2. Assignments
            var assignments = await _context.Assignments
                .Where(a =>
                    !a.IsDeleted &&
                    a.Course.Classes.Any(cl => cl.Users.Any(u => u.Id == userId)))
                .ToListAsync();

            // 3. Submissions
            var submissions = await _context.Submissions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.SubmittedAt)
                .Take(6)
                .ToListAsync();

            // 4. Due / Overdue
            var dueSoon = assignments
                .Where(a => a.Deadline >= now && (a.Deadline - now).TotalDays <= 7)
                .OrderBy(a => a.Deadline)
                .ToList();

            var overdue = assignments
                .Where(a => a.Deadline < now)
                .OrderByDescending(a => a.Deadline)
                .ToList();

            // 5. Progress (demo = 0)
            var courseProgress = courses.Select(c => new CourseProgressVm
            {
                CourseId = c.Id,
                CourseName = c.Name,
                ProgressPercent = 0 // TODO: nối lesson progress sau
            }).ToList();

            // 6. Build VM
            var vm = new StudentDashboardVm
            {
                TotalCourses = courses.Count,
                DueSoonCount = dueSoon.Count,
                OverdueCount = overdue.Count,
                RecentSubmissionCount = submissions.Count,

                AverageProgress = courseProgress.Any()
                    ? (int)Math.Round(courseProgress.Average(x => x.ProgressPercent))
                    : 0,

                CourseProgresses = courseProgress,

                DueSoonAssignments = dueSoon.Select(a => new AssignmentDashboardVM
                {
                    Id = a.Id,
                    Title = a.Title,
                    Deadline = a.Deadline,
                    CourseId = a.CourseId
                }).ToList(),

                OverdueAssignments = overdue.Select(a => new AssignmentDashboardVM
                {
                    Id = a.Id,
                    Title = a.Title,
                    Deadline = a.Deadline,
                    CourseId = a.CourseId
                }).ToList(),

                RecentSubmissions = submissions.Select(s => new SubmissionVm
                {
                    Id = s.Id,
                    SubmittedAt = s.SubmittedAt,
                    AssignmentId = s.AssignmentId
                }).ToList(),

                Calendar = []
            };

            return View(vm);
        }
    }

}
