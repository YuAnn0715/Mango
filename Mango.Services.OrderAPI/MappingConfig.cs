using AutoMapper;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Models.Dto;


namespace Mango.Services.OrderAPI
{
    // 自動映射器
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                // 反向映射
                Config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

                Config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                Config.CreateMap<OrderDetailsDto, CartDetailsDto>();

                Config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                Config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
