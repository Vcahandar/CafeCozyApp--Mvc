using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Areas.Admin.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Subtitle { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
    }
}
