using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EnglishCenterMVC.Areas.Student.Models;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class LessonController : Controller
    {
        ILessonService lessonService;

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var lesson = await lessonService.GetLessonAsync(id);
            if(lesson == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isCompleted =  lessonService.IsCompleted(id, userId);
            var vm = new LessonVM
            {
                IsCompleted = isCompleted,
                Description = lesson.Description,
                fileUrl = lesson.fileUrl,
                Id = lesson.Id,
                Order = lesson.Order,
                Title = lesson.Title,
                imageUrl = lesson.imageUrl,
                videoUrl = lesson.videoUrl,
                Section = lesson.Section,
            };
            return View(vm);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MarkCompleted(int lessonId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lesson = await lessonService.MarkCompleted(lessonId, userId);
            return RedirectToAction("Index", new { id = lesson.SectionId});
        }
    }
}
