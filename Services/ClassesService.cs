using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Services
{
    public class ClassesService : IClassesService
    {
        private readonly DataContext _context;
        private readonly UserManager<User> userManager;
        public ClassesService (DataContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        async Task<IEnumerable<Classes>> IClassesService.GetByUserId(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var classes = await _context.Classes
                .Where(c => c.Users.Contains(user))
                .ToListAsync();

            return classes;
        }

        async Task<IEnumerable<Classes>> IClassesService.GetByCourseId(int courseId)
        {
            var classes = await _context.Classes
                .Where(c => c.CourseId == courseId)
                .ToListAsync();

            return classes;
        }
    }
}
