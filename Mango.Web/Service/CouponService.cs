using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        // DI注入
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// 取得全部優惠券
        /// </summary>
        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        /// <summary>
        /// 依code取得優惠券
        /// </summary>
        /// <param name="couponCode">優惠券代碼</param>
        public async Task<ResponseDto?> GetCoupon(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        /// <summary>
        /// 依Id取得優惠券
        /// </summary>
        /// <param name="id">優惠券編號</param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        /// <summary>
        /// 建立新優惠券
        /// </summary>
        /// <param name="couponDto">優惠券請求內容</param>
        /// <returns></returns>
        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        /// <summary>
        /// 更新優惠券
        /// </summary>
        /// <param name="couponDto">優惠券請求內容</param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        /// <summary>
        /// 刪除優惠券
        /// </summary>
        /// <param name="id">優惠券編號</param>
        /// <returns></returns>
        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }
    }
}
