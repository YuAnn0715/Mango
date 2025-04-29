using Microsoft.AspNetCore.Identity;

namespace Mango.Service.AuthAPI.Migrations
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
