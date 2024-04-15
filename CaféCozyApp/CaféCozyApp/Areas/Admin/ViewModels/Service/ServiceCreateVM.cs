using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Areas.Admin.ViewModels.Service
{
    public class ServiceCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string IconUrl { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }

    }
}
