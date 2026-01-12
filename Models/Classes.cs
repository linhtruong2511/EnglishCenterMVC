namespace EnglishCenterMVC.Models
{
    public enum ClassStatus
    {
        Planned,    // Sắp mở
        Opening,    // Đang mở (đang nhận học viên)
        InProgress, // Đang học
        Ended,      // Đã kết thúc
        Cancelled   // Đã hủy
    }
    public class Classes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudent { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ClassStatus ClassStatus { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
