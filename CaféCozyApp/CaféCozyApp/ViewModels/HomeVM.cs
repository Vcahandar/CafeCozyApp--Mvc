using CaféCozyApp.Models;

namespace CaféCozyApp.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Product> Products { get; set; }


        public Reservation reservation { get; set; }


    }
}
