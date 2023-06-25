using Cars.Services.CouponAPI.Models.DTO;
using Cars.UI.Models;
using Cars.UI.Service.IService;
using Cars.UI.Utility;
using ResponseDTO = Cars.UI.Models.ResponseDTO;

namespace Cars.UI.Service
{
    public class AuthService : IAuthService
    {

        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = UI.Utility.ApiConstants.ApiTypeEnum.POST,
                Data = registrationRequestDTO,
                Url = ApiConstants.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = UI.Utility.ApiConstants.ApiTypeEnum.POST,
                Data = loginRequestDTO,
                Url = ApiConstants.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = UI.Utility.ApiConstants.ApiTypeEnum.POST,
                Data = registrationRequestDTO,
                Url = ApiConstants.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
