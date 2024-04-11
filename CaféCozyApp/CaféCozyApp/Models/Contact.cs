using CaféCozyApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Models
{
    public class Contact:BaseEntity
    {
        public string LocationAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
    }
}
