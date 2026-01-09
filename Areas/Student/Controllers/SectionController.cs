using Microsoft.AspNetCore.Mvc;
using EnglishCenterMVC.Services;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
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
            if (sections.Count() <= 0)
            {
                return NotFound();
            }
            return View(sections);
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
