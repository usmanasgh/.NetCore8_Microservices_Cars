using Cars.Services.Security.AuthAPI.Models;

namespace Cars.Services.Security.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
