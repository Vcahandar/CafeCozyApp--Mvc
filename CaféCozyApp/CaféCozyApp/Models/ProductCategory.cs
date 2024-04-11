using CaféCozyApp.Models.Common;

namespace CaféCozyApp.Models
{
    public class ProductCategory :BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
