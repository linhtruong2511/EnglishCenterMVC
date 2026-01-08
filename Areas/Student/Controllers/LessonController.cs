using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnglishCenter.Data;
using EnglishCenter.Model;
using EnglishCenter.Services;

namespace EnglishCenterMVC.Areas.Student.Controllers
{
    [Area("Student")]
    public class LessonController : Controller
    {
        ILessonService lessonService;

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var lessons = await lessonService.GetLessonAsync(id);
            if(lessons == null)
            {
                return NotFound();
            }
            return View(lessons);
        }
    }
}
