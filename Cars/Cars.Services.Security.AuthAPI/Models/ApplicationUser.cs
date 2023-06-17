using Microsoft.AspNetCore.Identity;

namespace Cars.Services.Security.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
