using Cars.UI.Models;
using Cars.UI.Service.IService;
using Cars.UI.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ResponseDTO result = await _authService.RegisterAsync(model);
            ResponseDTO assignRole;

            if(result != null && result.Success)
            {
                if(string.IsNullOrEmpty(model.Role))
                {
                    model.Role = ApiConstants.RoleCustomer;
                }
                
                assignRole = await _authService.AssignRoleAsync(model);
                
                if(assignRole != null && assignRole.Success)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
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
