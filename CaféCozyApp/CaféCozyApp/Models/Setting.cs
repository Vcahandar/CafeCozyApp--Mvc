using CaféCozyApp.Models.Common;

namespace CaféCozyApp.Models
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
