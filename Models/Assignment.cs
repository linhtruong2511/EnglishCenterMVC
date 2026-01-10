namespace EnglishCenterMVC.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TypeSubmit { get; set; }
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
