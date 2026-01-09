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
using EnglishCenterMVC.Areas.Admin.Models;

namespace EnglishCenterMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SectionsController : Controller
    {
        private ISectionService sectionService;
        private ICourseService courseService;
        

        public SectionsController(
            ISectionService sectionService,
            ICourseService courseService)
        {
            this.sectionService = sectionService;
            this.courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? courseId)
        {
            IEnumerable<Section> sections;

            if (courseId.HasValue)
            {
                sections = await sectionService.GetSectionsAsync(courseId.Value);
            }
            else
            {
                sections = await sectionService.GetSectionsAsync();
            }


            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            return View(sections);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await sectionService.GetSectionAsync(id.Value);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SectionCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var section = new Section
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    CourseId = vm.CourseId,
                    Order = vm.Order
                };
                await sectionService.AddSectionAsync(section);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await sectionService.GetSectionAsync(id.Value);
            if (section == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            ViewBag.Lessons = section.Lessons;
            return View(new SectionUpdateVM
            {
                Id = section.Id,
                Name = section.Name,
                Description = section.Description,
                Order = section.Order,
                CourseId = section.CourseId,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionUpdateVM vm)
        {
            ViewBag.Lessons = vm.Lessons;
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var section = new Section
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        CourseId = vm.CourseId,
                        Order = vm.Order,
                    };

                    section = await sectionService.UpdateSectionAsync(id, section);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { courseId = vm.CourseId});
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await sectionService.GetSectionAsync(id.Value);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await sectionService.GetSectionAsync(id);
            if (section != null)
            {
                await sectionService.DeleteSectionAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return sectionService.GetSectionAsync(id) is not null;
        }
    }
}
