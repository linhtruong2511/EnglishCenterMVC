using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.AspNetCore.Authorization;
using EnglishCenterMVC.Services;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class ClassesController : Controller
    {
        private readonly DataContext _context;
        private IAuthService authService;
        private IClassesService classesService;

        public ClassesController(
            DataContext context,
            IAuthService authService,
            IClassesService classesService)
        {
            _context = context;
            this.authService = authService;
            this.classesService = classesService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await authService.GetUser(User);

            var classes = await classesService.GetByUserId(currentUser.Id);
            return View(classes);
        }

        public async Task<IActionResult> Details(int? id)
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
    }
}
