using CaféCozyApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Models
{
    public class Message :BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
