using System.Runtime.CompilerServices;
using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly DataContext context;
        private readonly IFileService fileService;
        private readonly ICourseService courseService;

        public AssignmentService(
            DataContext context,
            IFileService fileService,
            ICourseService courseService)
        {
            this.context = context;
            this.fileService = fileService;
            this.courseService = courseService;
        }


        async Task<Assignment> IAssignmentService.AddAssignmentAsync(AssignmentCreateVM assignment)
        {
            if (assignment.File == null)
                throw new Exception("File bài tập không hợp lệ");

            string fileUrl = await fileService.SaveFileAsync(assignment.File, "files/assignments");

            var entity = new Assignment
            {
                Title = assignment.Title,
                Description = assignment.Description,
                TypeSubmit = assignment.Type,
                FileUrl = fileUrl,
                Deadline = assignment.Deadline,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CourseId = assignment.CourseId,
            };

            context.Assignments.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        async Task<Assignment> IAssignmentService.DeleteAssignmentAsync(int id)
        {
            var entity = await context.Assignments.FindAsync(id);
            if (entity == null)
                throw new Exception("Không tìm thấy bài tập");

            if (!string.IsNullOrEmpty(entity.FileUrl))
                fileService.Delete(entity.FileUrl);

            context.Assignments.Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        async Task<IEnumerable<Assignment>> IAssignmentService.GetAssignmentsAsync()
        {
            return await context.Assignments
                .Where(a => a.Deadline > DateTime.Now)
                .Include(a => a.Course)
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }

        async Task<IEnumerable<Assignment>> IAssignmentService.GetOverdueAssignmentsAsync()
        {
            return await context.Assignments
                .Where(a => a.Deadline <= DateTime.Now)
                .Include(a => a.Course)
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }

        async Task<Assignment> IAssignmentService.GetAssignmentAsync(int id)
        {
            var assignment =  await context.Assignments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (assignment is null) throw new Exception($"Không tìm thấy bài tập có id = {id}");
            return assignment;
        }

        async Task<IEnumerable<Assignment>> IAssignmentService.GetAssignmentsAsync(int courseId)
        {
            return await context.Assignments
                .Where(a => a.CourseId == courseId && a.Deadline >= DateTime.Now)
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }

        async Task<string> IAssignmentService.GetAssignmentContentAsync(int assignmentId)
        {
            var assignment = await context.Assignments.FindAsync(assignmentId);
            if (assignment is null) throw new Exception("Không tìm thấy bài tập");
            if (assignment.Deadline <= DateTime.Now) throw new Exception("Bài tập đã quá hạn nộp");
            return assignment.FileUrl;
        }

        async Task<Assignment> IAssignmentService.UpdateAssignmentAsync(int id, AssignmentUpdateVM assignment)
        {
            var entity = await context.Assignments.FindAsync(id);
            if (entity == null)
                throw new Exception("Không tìm thấy bài tập");

            entity.Title = assignment.Title;
            entity.Description = assignment.Description;
            entity.TypeSubmit = assignment.Type;
            entity.Deadline = assignment.Deadline;
            entity.UpdateAt = DateTime.Now;

            if (assignment.File != null)
            {
                if (!string.IsNullOrEmpty(entity.FileUrl))
                    fileService.Delete(entity.FileUrl);

                entity.FileUrl = await fileService.SaveFileAsync(assignment.File, "files/assignments");
            }

            await context.SaveChangesAsync();
            return entity;
        }

        async Task<int> IAssignmentService.GetNewlyUploadedAssignmentsAsync()
        {
            return await context.Assignments
                .Where(a => (a.CreateAt - DateTime.Now).TotalDays < 3)
                .CountAsync();
        }
        async Task<int> IAssignmentService.GetAvailableAssignmentsAsync()
        {
            return await context.Assignments
                .Where(a => a.Deadline > DateTime.Now)
                .CountAsync();
        }
    }
}
