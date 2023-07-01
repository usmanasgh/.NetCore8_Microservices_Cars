using Cars.UI.Models;
using Cars.UI.Service.IService;
using Cars.UI.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cars.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO requestDTO = new LoginRequestDTO();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            ResponseDTO responseDTO = await _authService.LoginAsync(model);

            if (responseDTO != null && responseDTO.Success)
            {
                LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(responseDTO.Result));

                await SignInUser(loginResponseDTO);
                _tokenProvider.SetToken(loginResponseDTO.Token);
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDTO.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = ApiConstants.RoleAdmin, Value = ApiConstants.RoleAdmin},
                new SelectListItem{Text = ApiConstants.RoleCustomer, Value = ApiConstants.RoleCustomer}
            };
            
            ViewBag.RoleList = roleList;   

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            if(ModelState.IsValid) {

                ResponseDTO result = await _authService.RegisterAsync(model);
                ResponseDTO assignRole;

                if (result != null && result.Success)
                {
                    if (string.IsNullOrEmpty(model.Role))
                    {
                        model.Role = ApiConstants.RoleCustomer;
                    }

                    assignRole = await _authService.AssignRoleAsync(model);

                    if (assignRole != null && assignRole.Success)
                    {
                        TempData["success"] = "Registration Successful";
                        return RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    ModelState.AddModelError("CustomError", result.Message);
                }
            }
            
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = ApiConstants.RoleAdmin, Value = ApiConstants.RoleAdmin},
                new SelectListItem{Text = ApiConstants.RoleCustomer, Value = ApiConstants.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }

        private async Task SignInUser(LoginResponseDTO model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                        jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                        jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                        jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email).Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }

    }
}
