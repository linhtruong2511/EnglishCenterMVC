using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Student.Models
{
    public class LessonVM : Lesson
    {
        public bool IsCompleted { get; set; }
    }
}
