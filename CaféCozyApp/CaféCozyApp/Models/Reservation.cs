using CaféCozyApp.Models.Common;

namespace CaféCozyApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
