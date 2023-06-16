using Cars.Services.CouponAPI.Utility;
using Cars.UI.Models;
using Cars.UI.Service.IService;

namespace Cars.UI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        
        public CouponService(IBaseService baseService) 
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.POST,
                Data = couponDTO,
                Url = ApiConstants.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTO?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.DELETE,
                Url = ApiConstants.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.GET,
                Url = ApiConstants.CouponAPIBase+"/api/coupon"
            });
        }

        public async Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.GET,
                Url = ApiConstants.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDTO?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.GET,
                Url = ApiConstants.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = Services.CouponAPI.Utility.ApiConstants.ApiTypeEnum.PUT,
                Data = couponDTO,
                Url = ApiConstants.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
