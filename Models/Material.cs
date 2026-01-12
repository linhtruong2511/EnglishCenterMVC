namespace EnglishCenterMVC.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string FileName { get; set; }    
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedAt { get; set; }    

    }
}
