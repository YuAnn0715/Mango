using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    // 自動映射器
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<CouponDto, Coupon>();
                Config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
