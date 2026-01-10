using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Student.Models
{
    public class AssignmentVM
    {
        public int NewlyUploaded { get; set; }
        public int Available { get; set; } 
        public IEnumerable<Assignment> AvailableAssignments { get; set; }
        public IEnumerable<Assignment> OverdueAssignments { get; set; }
    }
}
