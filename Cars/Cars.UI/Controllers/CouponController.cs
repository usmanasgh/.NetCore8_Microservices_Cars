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
            else
            {
                TempData["error"] = responseDTO?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if(ModelState.IsValid)
            {
                ResponseDTO? response = await _couponService.CreateCouponAsync(model);

                if(response != null && response.Success) {

                    TempData["success"] = "Coupon created successfully";

                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int CouponId)
        {
            List<CouponDTO>? list = new();

            ResponseDTO? responseDTO = await _couponService.GetCouponByIdAsync(CouponId);

            if (responseDTO != null && responseDTO.Success)
            {
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(responseDTO.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = responseDTO?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
        {
            List<CouponDTO>? list = new();

            ResponseDTO? responseDTO = await _couponService.DeleteCouponAsync(couponDTO.CouponId);

            if (responseDTO != null && responseDTO.Success)
            {
                TempData["success"] = "Coupon deleted.";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDTO?.Message;
            }

            return View(couponDTO);
        }
    }
}
