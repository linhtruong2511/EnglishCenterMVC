namespace EnglishCenterMVC.DTO
{
    public class LessonUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Video { get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? Image { get; set; }
        public int? Order { get; set; }
        public int SectionId {  get; set; }
    }
}
