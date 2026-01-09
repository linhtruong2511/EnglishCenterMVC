using System.ComponentModel.DataAnnotations;
using Humanizer.Localisation.DateToOrdinalWords;

namespace EnglishCenterMVC.Areas.Admin.Models
{
    public class SectionCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
    }
}
