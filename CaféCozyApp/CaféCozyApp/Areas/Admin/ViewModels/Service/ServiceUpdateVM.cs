namespace CaféCozyApp.Areas.Admin.ViewModels.Service
{
    public class ServiceUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Photo { get; set; }

        public string IconUrl { get; set; }
    }
}
