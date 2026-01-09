using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenterMVC.Models;
using EnglishCenterMVC.Services;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class CoursesController : Controller
    {
        ICourseService courseService;
        ICategoryService categoryService;

        public CoursesController(ICourseService course,
            ICategoryService category)
        {
            this.courseService = course;
            this.categoryService = category;
        }

        public async Task<IActionResult> Index(string name = "")
        {
            var course = await courseService.GetCourses(name);
            return View(course);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await courseService.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await courseService.AddCourse(course);
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(), "Id", "Id", course.CategoryId);
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await courseService.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(), "Id", "Id", course.CategoryId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await courseService.UpdateCourse(id ,course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(), "Id", "Id", course.CategoryId);
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await courseService.DeleteCourse(id.Value);
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await courseService.GetCourseById(id);
            if (course != null)
            {
                await courseService.DeleteCourse(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            var course = courseService.GetCourseById(id);

            if (course is null) return false;
            else return true;
        }
    }
}
