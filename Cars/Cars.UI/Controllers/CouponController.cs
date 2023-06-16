using Cars.UI.Models;
using Cars.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cars.UI.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new();

            ResponseDTO? responseDTO = await _couponService.GetAllCouponsAsync();
            
            if(responseDTO != null && responseDTO.Success)
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(responseDTO.Result));
            }

            return View(list);
        }
    }
}
