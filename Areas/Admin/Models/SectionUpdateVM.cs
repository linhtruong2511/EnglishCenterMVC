using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class SectionUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
