using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Services;

namespace EnglishCenterMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        public CoursesController(ICourseService course,
            IFileService fileService,
            ICategoryService categoryService)
        {
            _courseService = course;
            _fileService = fileService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string name = "")
        {
            var course = await _courseService.GetCourses(name);
            return View(course);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM vm, string name = "")
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    Sale = vm.Sale,
                    CategoryId = vm.CategoryId,
                };
                if (vm.Image is not null)
                {
                    var imageUrl = await _fileService.SaveImageAsync(vm.Image, "images/courses");
                    course.ImageUrl = imageUrl;
                }
                await _courseService.AddCourse(course);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "Id", "Name", vm.CategoryId);
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "Id", "Name", course.CategoryId);
            return View(new CourseUpdateVM
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                CategoryId = course.CategoryId,
                Price = course.Price,
                Sale = course.Sale,
                ImageURL = course.ImageUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseUpdateVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
                }

            if (ModelState.IsValid)
            {
                try
                {
                    var course = new Course
                    {
                        Id = id,
                        Name = vm.Name,
                        Description = vm.Description,
                        CategoryId = vm.CategoryId,
                        Price = vm.Price,
                        Sale = vm.Sale,
                        ImageUrl = vm.ImageURL
                    };
                    if (vm.Image is not null)
                    {
                        var imageUrl = await _fileService.SaveImageAsync(vm.Image, "images/courses");
                        course.ImageUrl = imageUrl;
                    }

                    await _courseService.UpdateCourse(id, course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(""), "Id", "Name", vm.CategoryId);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course != null)
            {
                await _courseService.DeleteCourse(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _courseService.GetCourseById(id) is not null;
        }
    }
}
