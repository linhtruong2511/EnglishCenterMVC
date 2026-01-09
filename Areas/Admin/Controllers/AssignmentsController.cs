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
    public class AssignmentsController : Controller
    {
        private IAssignmentService assignmentService;
        private ICourseService courseService;

        public AssignmentsController(IAssignmentService assignmentService,
            ICourseService courseService)
        {
            this.assignmentService = assignmentService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await assignmentService.GetAssignmentsAsync();
            return View(assignments);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await assignmentService.GetAssignmentAsync(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentCreateVM assignment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await assignmentService.AddAssignmentAsync(assignment);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình tạo: " + ex.Message);
                    ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name", assignment.CourseId);
                    return View(assignment);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name", assignment.CourseId);
            return View(assignment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await assignmentService.GetAssignmentAsync(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name", assignment.CourseId);
            return View(new AssignmentUpdateVM
            {
                Id = assignment.Id,
                Title = assignment.Title,
                Description = assignment.Description,
                FileUrl = assignment.FileUrl,
                Type = assignment.Type,
                Deadline = assignment.Deadline,
                CourseId = assignment.CourseId,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignmentUpdateVM assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await assignmentService.UpdateAssignmentAsync(id, assignment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AssignmentExists(assignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi lưu " + ex.Message);
                    return View(assignment);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(await courseService.GetCourses(), "Id", "Name", assignment.CourseId);
            return View(assignment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await assignmentService.GetAssignmentAsync(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await assignmentService.GetAssignmentAsync(id);
            if (assignment != null)
            {
                try
                {
                    await assignmentService.DeleteAssignmentAsync(id);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi xóa " + ex.Message);
                    return View(assignment);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AssignmentExists(int id)
        {
            return await assignmentService.GetAssignmentAsync(id) is not null;
        }
    }
}
