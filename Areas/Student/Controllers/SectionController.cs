using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;
using EnglishCenterMVC.Areas.Student.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class SectionController : Controller
    {
        ISectionService sectionService;
        ILessonService lessonService;

        public SectionController(ISectionService sectionService, ILessonService lessonService)
        {
            this.sectionService = sectionService;
            this.lessonService = lessonService;
        }

        public async Task<IActionResult> Index(int courseid)
        {
            var sections = await sectionService.GetSectionsAsync(courseid);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<SectionVM> vms = new List<SectionVM>();
            foreach (var item in sections)
            {
                vms.Add(new SectionVM
                {
                    Name = item.Name,
                    Course = item.Course,
                    Description = item.Description,
                    Id = item.Id,
                    Order = item.Order,
                    Lessons = item.Lessons.Select(l => new LessonVM
                    {
                        Id = l.Id,
                        Title = l.Title,
                        IsCompleted = lessonService.IsCompleted(l.Id, userId)
                    }).ToList()
                });
            }

            return View(vms);
        }

        public async Task<IActionResult> Lesson(int sectionid)
        {
            if (sectionid <= 0)
            {
                return BadRequest();
            }
            try
            {
                var lesson = await lessonService.GetLessonAsync(sectionid);
                return RedirectToAction("Index", "Lesson", new { area = "Student", id = lesson.Id });
            } catch
            {
                return NotFound();
            }
        }
    }
}
