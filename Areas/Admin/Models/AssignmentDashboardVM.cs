namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class AssignmentDashboardVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime Deadline { get; set; }
        public int CourseId { get; set; }
    }
}
