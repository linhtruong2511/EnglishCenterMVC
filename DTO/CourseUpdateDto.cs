using System.ComponentModel.DataAnnotations;

namespace EnglishCenterMVC.DTO
{
    public class CourseUpdateDto
    {
        [Required, MinLength(4, ErrorMessage = "Tên khóa học phải dài hơn 4 ký tự")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Mô tả khóa học phải dài hơn 4 ký tự")]
        public string Description { get; set; }
    }
}
