using CaféCozyApp.Models.Common;

namespace CaféCozyApp.Models
{
    public class Reservation:BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
