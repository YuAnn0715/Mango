using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
        // 依code取得優惠券
        Task<ResponseDto?> GetCoupon(string couponCode);

        // 取得全部優惠券
        Task<ResponseDto?> GetAllCouponsAsync();

        // 依Id取得優惠券
        Task<ResponseDto?> GetCouponByIdAsync(int id);

        // 建立新優惠券
        Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);

        // 更新優惠券
        Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);

        // 刪除優惠券
        Task<ResponseDto?> DeleteCouponsAsync(int id);

    }
}
