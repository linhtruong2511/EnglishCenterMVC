namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class SubmissionVm
    {
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int AssignmentId { get; set; }
    }
}
