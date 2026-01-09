namespace EnglishCenterMVC.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public string FileUrl { get; set; }

        public int Score { get; set; }
        public string Feedback { get; set; }
        public DateTime GradedAt { get; set; }
    }
}
