using CaféCozyApp.Models;

namespace CaféCozyApp.ViewModels
{
    public class ContactVM
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public Message message { get; set; }
    }
}
