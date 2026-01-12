using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassesController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public ClassesController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Classes.Include(c => c.Course);
            return View(await dataContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.ClassesStatus = new SelectList(Enum.GetValues(typeof(ClassStatus))
                    .Cast<ClassStatus>()
                    .Select(s => new {
                        Id = (int)s,
                        Name = s.ToString()
                    })
                    .ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassesCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Classes
                {
                    Name = vm.Name,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    MaxStudent = vm.MaxStudent,
                    CourseId = vm.CourseId,
                    ClassStatus = vm.ClassStatus
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", vm.CourseId);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classes == null)
            {
                return NotFound();
            }

            return View(classes);
        }

        public async Task<IActionResult> AddStudent(int classId)
        {
            var classes = await _context.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == classId);
            var existingStudentIds = _context.Users
                .Where(u => u.Classes.FirstOrDefault(c => c.Id == classId) != null)
                .Select(u => u.Id)
                .ToList();
            var students = await _userManager.GetUsersInRoleAsync("Student");
            var availableStudents = students
                .Where(u => !existingStudentIds.Contains(u.Id))
                .Select(u => new {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName} ({u.Email})"
                }).ToList();
            ViewBag.Students = new SelectList(availableStudents, "Id", "FullName");
            return View(classes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmAddStudent(int classId, string studentId)
        {
            if (string.IsNullOrEmpty(studentId)) return RedirectToAction(nameof(AddStudent), new { classId });

            var user = await _userManager.FindByIdAsync(studentId);
            var classes = await _context.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (user is null || classes is null)
            {
                return NotFound();
            }

            classes.Users.Add(user);

            await _context.SaveChangesAsync();
            return RedirectToAction("AddStudent", "Classes", new { classId = classId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmRemoveStudent(int classId, string studentId)
        {
            if (string.IsNullOrEmpty(studentId)) return RedirectToAction(nameof(AddStudent), new { classId });

            var user = await _userManager.FindByIdAsync(studentId);
            var classes = await _context.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (user is null || classes is null)
            {
                return NotFound();
            }

            classes.Users.Remove(user);

            await _context.SaveChangesAsync();
            return RedirectToAction("AddStudent", "Classes", new { classId = classId });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (classes != null)
            {
                _context.Classes.Remove(classes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
