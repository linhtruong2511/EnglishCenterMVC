namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class CourseCreateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int Price { get; set; }
        public int Sale { get; set; }
        public int CategoryId { get; set; }
    }
}
