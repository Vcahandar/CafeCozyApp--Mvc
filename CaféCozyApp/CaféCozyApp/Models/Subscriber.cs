using CaféCozyApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Models
{
    public class Subscriber:BaseEntity
    {
        public string Email { get; set; }
    }
}
