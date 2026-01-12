using EnglishCenterMVC.DTO;
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetLessonsAsync(int sectionId);
        Task<IEnumerable<Lesson>> GetLessonsAsync();
        Task<Lesson> GetLessonAsync(int id);
        Task<Lesson> AddLessonAsync(LessonCreateDto lesson);
        Task<Lesson> UpdateLessonAsync(int id, LessonUpdateDto lesson);
        Task DeleteLessonAsync(int id);
        Task<Lesson> MarkCompleted(int lessonId, string userId);
        Task<bool> IsCompletedAsync(int lessonId, string userId);
        bool IsCompleted(int lessonId, string userId);
    }
}
