using CaféCozyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int CategoryId { get; set; }
    }
}
