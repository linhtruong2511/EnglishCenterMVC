using EnglishCenterMVC.Areas.Student.Models;
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class StudentDashboardVm
    {
        public int TotalCourses { get; set; }
        public int DueSoonCount { get; set; }
        public int OverdueCount { get; set; }
        public int RecentSubmissionCount { get; set; }

        // Progress
        public int AverageProgress { get; set; }
        public List<CourseProgressVm> CourseProgresses { get; set; } = [];

        // Assignment
        public List<AssignmentDashboardVM> DueSoonAssignments { get; set; } = [];
        public List<AssignmentDashboardVM> OverdueAssignments { get; set; } = [];

        // Submission
        public List<SubmissionVm> RecentSubmissions { get; set; } = [];

        // Calendar
        public List<CalendarDayVm> Calendar { get; set; } = [];
    }

    public class CourseProgressVm
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = "";
        public int ProgressPercent { get; set; }
    }

    public class CalendarDayVm
    {
        public DateTime Date { get; set; }
        public int DueCount { get; set; }
        public DashboardDueLevel Level { get; set; }
    }

    public enum DashboardDueLevel
    {
        None,
        Ok,
        Warning,
        Danger
    }
}
