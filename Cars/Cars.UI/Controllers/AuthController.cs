using Cars.UI.Models;
using Cars.UI.Service.IService;
using Cars.UI.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cars.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
        public IActionResult Logout()
        {
            return View();
        }
    }
}
