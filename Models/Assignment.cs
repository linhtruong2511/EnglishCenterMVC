namespace EnglishCenterMVC.Models
{
    public enum SubmitType
    {
        ONLINE,
        FILE
    }

    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SubmitType TypeSubmit { get; set; } = SubmitType.FILE;
        public string FileUrl { get; set; }

        public int CourseId{ get; set; }
        public Course Course { get; set; }

        public bool IsDeleted { get; set; }
        public bool AllowResubmit { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

    }
}
