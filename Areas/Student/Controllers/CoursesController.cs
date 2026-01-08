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

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class CoursesController : Controller
    {
        ICourseService courseService;
        ICategoryService categoryService;

        public CoursesController(ICourseService course)
        {
            this.courseService = course;
        }

        // GET: Student/Courses
        public async Task<IActionResult> Index(string name = "")
        {
            var course = await courseService.GetCourses(name);
            return View(course);
        }

        // GET: Student/Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Student/Courses/Create
        public async Task<IActionResult> Create(string name = "")
        {
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(name), "Id", "Id");
            return View();
        }

        // POST: Student/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, string name = "")
        {
            if (ModelState.IsValid)
            {
                await courseService.AddCourse(course);
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(name), "Id", "Id", course.CategoryId);
            return View(course);
        }

        // GET: Student/Courses/Edit/5
        public async Task<IActionResult> Edit(int id, string name = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(name), "Id", "Id", course.CategoryId);
            return View(course);
        }

        // POST: Student/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, string name = "")
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
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategories(name), "Id", "Id", course.CategoryId);
            return View(course);
        }

        // GET: Student/Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await courseService.DeleteCourse(id);
            return View();
        }

        // POST: Student/Courses/Delete/5
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
