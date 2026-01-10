using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class AssignmentUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SubmitType SubmitType { get; set; }
        public string FileUrl { get; set; }
        public IFormFile? File { get; set; }
        public DateTime Deadline { get; set; }
        public int CourseId {  get; set; }
    }
}
