using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        // 取得對應的優惠券
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
