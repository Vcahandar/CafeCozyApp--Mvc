using Microsoft.AspNetCore.Identity;

namespace CaféCozyApp.Models.Identity
{
    public class AppUser: IdentityUser
    {
        public string Fullname { get; set; }

    }
}
