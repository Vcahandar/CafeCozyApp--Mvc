using CaféCozyApp.Models;

namespace CaféCozyApp.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public IEnumerable<Service> Services { get; set; }

        public Reservation reservation { get; set; }


    }
}
