using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Services
{
    public interface ISubmissionService
    {
        Task<Submission> SubmitAssignment(
            int assignmentId,
            string userId,
            IFormFile file);

        Task<Submission> GradeSubmission(
            int submissionId,
            int score,
            string feedback);

        Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId);

        Task<IEnumerable<Submission>> GetSubmissionsByStudent(string userId);

        Task<Submission> GetSubmission(int id);
    }
}
