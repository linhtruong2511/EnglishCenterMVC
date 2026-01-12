using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class ClassesCreateVM
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudent { get; set; }
        public int CourseId { get; set; }
        public ClassStatus ClassStatus { get; set; }
    }
}
