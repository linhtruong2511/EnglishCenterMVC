using System.ComponentModel.DataAnnotations;
using EnglishCenterMVC.Models;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class AssignmentCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SubmitType SubmitType { get; set; }
        [Required(ErrorMessage = "Yêu cầu đính kèm file bài tập")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Yêu cầu phải có thời gian hết hạn")]
        public DateTime Deadline { get; set; }
        public int CourseId { get; set; }
    }
}
