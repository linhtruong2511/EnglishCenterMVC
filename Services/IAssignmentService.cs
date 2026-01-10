using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync(int courseId);
        Task<IEnumerable<Assignment>> GetAssignmentsAsync();
        Task<IEnumerable<Assignment>> GetOverdueAssignmentsAsync();
        Task<int> GetNewlyUploadedAssignmentsAsync();
        Task<int> GetAvailableAssignmentsAsync();
        Task<Assignment> GetAssignmentAsync(int id);
        Task<string> GetAssignmentContentAsync(int assignmentId); // Trả về đường dẫn file bài tập

        Task<Assignment> AddAssignmentAsync(AssignmentCreateVM assignment);
        Task<Assignment> UpdateAssignmentAsync(int id, AssignmentUpdateVM assignment);
        Task<Assignment> DeleteAssignmentAsync(int id);
    }
}
