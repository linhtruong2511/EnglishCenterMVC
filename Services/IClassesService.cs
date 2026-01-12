
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface IClassesService
    {
        Task<IEnumerable<Classes>> GetByUserId(string userId);  
        Task<IEnumerable<Classes>> GetByCourseId(int courseId);  
    }
}
