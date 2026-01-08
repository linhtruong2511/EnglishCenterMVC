using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using EnglishCenterMVC.DTO;
using EnglishCenterMVC.Services;
    
namespace EnglishCenterMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LessonsController : Controller
    {
        private ILessonService lessonService;
        private ISectionService sectionService;

        public LessonsController(
            ILessonService lessonService,
            ISectionService sectionService)
        {
            this.lessonService = lessonService;
            this.sectionService = sectionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Lesson> lessons = await lessonService.GetLessonsAsync();
            return View(lessons);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await lessonService.GetLessonAsync(id.Value);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        public async Task<IActionResult> Create(int sectionId)
        {
            ViewBag.sectionId = sectionId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( LessonCreateDto vm)
        {
            if (ModelState.IsValid)
            {
                await lessonService.AddLessonAsync(vm);
                return Redirect($"/admin/sections/edit/{vm.SectionId}");
            }
            return View(vm);
        }

        // GET: Admin/Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await lessonService.GetLessonAsync(id.Value);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(new LessonUpdateDto
            {
                Description = lesson.Description,
                Title = lesson.Title,
                Order = lesson.Order,
                Id = lesson.Id,
                SectionId = lesson.SectionId
            });
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LessonUpdateDto vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await lessonService.UpdateLessonAsync(id, vm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LessonExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/admin/sections/edit/{vm.SectionId}");
            }
            return View(vm);
        }

        // GET: Admin/Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await lessonService.GetLessonAsync(id.Value);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Admin/Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await lessonService.GetLessonAsync(id);
            if (lesson != null)
            {
                await lessonService.DeleteLessonAsync(id);
                return Redirect($"/admin/sections/edit/{lesson.SectionId}");
            }
            return NotFound();
        }

        private async Task<bool> LessonExists(int id)
        {
            return await lessonService.GetLessonAsync(id) is not null;
        }
    }
}
