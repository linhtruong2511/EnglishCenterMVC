using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(string? name = "");
        Task<Category> GetCategory(int id);
        Task<Category> UpdateCategory(int id, Category category);
        Task<Category> CreateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
