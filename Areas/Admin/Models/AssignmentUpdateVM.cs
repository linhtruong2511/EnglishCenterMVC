namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class AssignmentUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string FileUrl { get; set; }
        public IFormFile? File { get; set; }
        public DateTime Deadline { get; set; }
        public int CourseId {  get; set; }
    }
}
