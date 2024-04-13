using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Areas.Admin.ViewModels.Contact
{
    public class ContactCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string LocationAdress { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string EmailAdress { get; set; }
    }
}
