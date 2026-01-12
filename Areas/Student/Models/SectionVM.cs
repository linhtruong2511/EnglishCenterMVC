using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Student.Models
{
    public class SectionVM : Section
    {
        public List<LessonVM> Lessons { get; set; }
    }

}
