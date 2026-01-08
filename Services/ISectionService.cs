using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface ISectionService
    {
        Task<IEnumerable<Section>> GetSectionsAsync(int courseId);
        Task<IEnumerable<Section>> GetSectionsAsync();
        Task<Section> GetSectionAsync(int id);
        Task<Section> AddSectionAsync(Section section);
        Task<Section> UpdateSectionAsync(int id, Section section);
        Task DeleteSectionAsync(int id);
    }
}
