using Cars.Services.CouponAPI.DAL;
using Cars.Services.CouponAPI.Models;
using Cars.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDTO _responseDTO;

        public CouponAPIController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _appDbContext.Coupons.ToList();
                _responseDTO.Result = couponList;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon singleCoupon = _appDbContext.Coupons.First(x => x.CouponId == id);
                _responseDTO.Result = singleCoupon;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
    }
}
