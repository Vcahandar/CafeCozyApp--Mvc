using CaféCozyApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace CaféCozyApp.Models
{
    public class AboutFeature:BaseEntity
    {
        public string TitleFirst { get; set; }
        public string TitleSecond { get; set; }
        public string TitleThird { get; set; }
        public string DescriptionFirst { get; set; }
        public string DescriptionSecond { get; set; }
        public string DescriptionThird { get; set; }
        public string AddedTextFirst { get; set; }
        public string AddedTextSecond { get; set; }
        public string AddedTextThird { get; set; }
        public string ImageUrl { get; set; }
    }
}
