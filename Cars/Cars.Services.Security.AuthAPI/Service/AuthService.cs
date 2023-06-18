using Cars.Services.Security.AuthAPI.DAL;
using Cars.Services.Security.AuthAPI.Models;
using Cars.Services.Security.AuthAPI.Models.DTO;
using Cars.Services.Security.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Cars.Services.Security.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        // MUA: Use .Net built-in methods to complete registration password hashing and login details etc
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _appDbContext.applicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            if(user != null)
            {
                if(! _roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // MUA: Create new role which doesn't exist in the system
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult(); 
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if(user == null || isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }

            // MUA: if user was found, Generate JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDTO userDTO = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDTO;

        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                
                if(result.Succeeded)
                {
                    var userToReturn = _appDbContext.applicationUsers.First(x => x.UserName == registrationRequestDTO.Email);

                    UserDTO userDTO = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

            }
            return "Error encountered";
        }
    }
}
