using EnglishCenterMVC.Areas.Admin.Models;
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync(int courseId);
        Task<IEnumerable<Assignment>> GetAssignmentsAsync();


        Task<Assignment> AddAssignmentAsync(AssignmentCreateVM assignment);
        Task<Assignment> UpdateAssignmentAsync(int id, AssignmentUpdateVM assignment);
        Task<Assignment> DeleteAssignmentAsync(int id);
    }
}
