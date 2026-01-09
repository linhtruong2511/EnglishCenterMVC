using EnglishCenterMVC.Data;
using EnglishCenterMVC.DTO;
using EnglishCenterMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Services
{
    public class LessonService : ILessonService
    {
        private readonly DataContext _context;
        private IFileService fileService;

        public LessonService(
            DataContext context,
            IFileService fileService)
        {
            _context = context;
            this.fileService = fileService;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync(int sectionId)
        {
            return await _context.Lessons
                .Where(l => l.SectionId == sectionId)
                .Include(l => l.SectionId)
                .OrderBy(l => l.Order)
                .ToListAsync();
        }

        public async Task<Lesson> GetLessonAsync(int id)
        {
            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                throw new KeyNotFoundException($"Lesson {id} not found");

            return lesson;
        }

        public async Task<Lesson> AddLessonAsync(LessonCreateDto dto)
        {
            var lesson = new Lesson
            {
                SectionId = dto.SectionId,
                Title = dto.Title,
                Description = dto.Description,
                Order = dto.Order ?? _context.Lessons
                    .Where(l => l.SectionId == dto.SectionId)
                    .Count() + 1,
            };

            if (dto.File != null)
            {
                var fileUrl = await fileService.SaveFileAsync(dto.File, "files/lessons"); 
                lesson.fileUrl = fileUrl;
            }
            if (dto.Image != null)
            {
                var imageUrl = await fileService.SaveImageAsync(dto.Image, "images/lessons"); 
                lesson.imageUrl = imageUrl;
            }
            if (dto.Video != null)
            {
                var videoUrl = await fileService.SaveVideoAsync(dto.Video, "videos/lessons"); 
                lesson.videoUrl = videoUrl;
            }
            
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> UpdateLessonAsync(int id, LessonUpdateDto dto)
        {
            var existing = await _context.Lessons.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Lesson {id} not found");

            existing.Title = dto.Title ?? existing.Title;
            existing.Description = dto.Description ?? existing.Description;
            existing.Order = dto.Order ?? existing.Order;

            if (dto.File != null)
            {
                var fileUrl = await fileService.SaveFileAsync(dto.File, "files/lessons");
                fileService.Delete(existing.fileUrl);
                existing.fileUrl = fileUrl;
            }
            if (dto.Image != null)
            {
                var imageUrl = await fileService.SaveImageAsync(dto.Image, "images/lessons");
                fileService.Delete(existing.imageUrl);
                existing.imageUrl = imageUrl;
            }
            if (dto.Video != null)
            {
                var videoUrl = await fileService.SaveVideoAsync(dto.Video, "videos/lessons");
                fileService.Delete(existing.videoUrl);
                existing.videoUrl = videoUrl;
            }

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteLessonAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson {id} not found");

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            return await _context.Lessons
                .Include(l => l.Section)
                .OrderBy(l => l.Section.Order)
                .ToListAsync();
        }
    }
}
