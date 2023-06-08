using AutoMapper;
using Cars.Services.CouponAPI.Models;
using Cars.Services.CouponAPI.Models.DTO;

namespace Cars.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            });

            return mappingConfig;
        }
    }
}
