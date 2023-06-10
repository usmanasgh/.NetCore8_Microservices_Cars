using AutoMapper;
using Cars.Services.CouponAPI.DAL;
using Cars.Services.CouponAPI.Models;
using Cars.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cars.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _responseDTO = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _appDbContext.Coupons.ToList();
                _responseDTO.Result = _mapper.Map<IEnumerable<CouponDTO>>(couponList);
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
                _responseDTO.Result = _mapper.Map<CouponDTO>(singleCoupon); 
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon singleCoupon = _appDbContext.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                
                _responseDTO.Result = _mapper.Map<CouponDTO>(singleCoupon);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _appDbContext.Coupons.Add(coupon);
                _appDbContext.SaveChanges();
                _responseDTO.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _appDbContext.Coupons.Update(coupon);
                _appDbContext.SaveChanges();
                _responseDTO.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpDelete]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon coupon = _appDbContext.Coupons.First(u => u.CouponId == id);
                _appDbContext.Coupons.Remove(coupon);
                _appDbContext.SaveChanges();
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
