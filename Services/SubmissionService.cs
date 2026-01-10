using EnglishCenterMVC.Data;
using EnglishCenterMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenterMVC.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly DataContext context;
        private readonly IFileService fileService;
        //private readonly IGradingService gradingService;

        public SubmissionService(
        DataContext context,
        IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
            //this.gradingService = gradingService;
        }

        public async Task<Submission> SubmitAssignment(
            int assignmentId,
            string userId,
            IFormFile file)
        {
            if (file == null)
                throw new Exception("File nộp không hợp lệ");

            var assignment = await context.Assignments.FindAsync(assignmentId);
            if (assignment == null)
                throw new Exception("Không tìm thấy bài tập");

            if (DateTime.Now > assignment.Deadline)
                throw new Exception("Đã quá hạn nộp bài");

            var existingSubmission = await context.Submissions
                .Where(s => s.UserId == userId && s.AssignmentId == assignmentId)
                .FirstAsync();

            if (existingSubmission is not null)
            {
                if (!assignment.AllowResubmit)
                    throw new Exception("Bài tập này không được phép nộp lại");
                else
                {
                    existingSubmission.FileUrl = await fileService.SaveFileAsync(
                        file,
                        $"submissions/{assignmentId}/{userId}");
                    existingSubmission.SubmittedAt = DateTime.Now;
                    existingSubmission.Score = 0;
                    context.Update(existingSubmission);
                    await context.SaveChangesAsync();
                    return existingSubmission;
                }

            }
            var fileUrl = await fileService.SaveFileAsync(
                file,
                $"submissions/{assignmentId}/{userId}");

            var submission = new Submission
            {
                AssignmentId = assignmentId,
                UserId = userId,
                FileUrl = fileUrl,
                SubmittedAt = DateTime.Now,
                Score = 0
            };

            context.Submissions.Add(submission);
            await context.SaveChangesAsync();

            //await gradingService.RequestGrading(submission.Id);

            return submission;
        }

        public async Task<Submission> GradeSubmission(
            int submissionId,
            int score,
            string feedback)
        {
            var submission = await context.Submissions.FindAsync(submissionId);
            if (submission == null)
                throw new Exception("Không tìm thấy bài nộp");

            submission.Score = score;
            submission.Feedback = feedback;
            submission.GradedAt = DateTime.Now;

            await context.SaveChangesAsync();
            return submission;
        }

        public async Task<IEnumerable<Submission>> GetSubmissions()
        {
            return await context.Submissions
                .Include(x => x.User)
                .Include(x => x.Assignment)
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId)
        {
            return await context.Submissions
                .Where(x => x.AssignmentId == assignmentId)
                .Include(x => x.User)
                .Include(x => x.Assignment)
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByStudent(string userId)
        {
            return await context.Submissions
                .Where(x => x.UserId == userId)
                .Include(x => x.Assignment)
                .OrderByDescending(x => x.SubmittedAt)
                .ToListAsync();
        }

        public async Task<Submission> GetSubmission(int id)
        {
            return await context.Submissions
                .Include(x => x.Assignment)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
