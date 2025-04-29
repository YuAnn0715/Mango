using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// 依userId取得對應購物車
        /// </summary>
        /// <param name="userId">使用者ID</param>
        /// <returns></returns>

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
            }); ;
        }

        /// <summary>
        /// 新增更改購物車
        /// </summary>
        /// <param name="cartDto">購物車內容</param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
        }

        /// <summary>
        /// 刪除購物車內容
        /// </summary>
        /// <param name="cartDetailsId">購物車內容產品編號</param>
        /// <returns></returns>
        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="cartDto">購物車內容</param>
        /// <returns></returns>
        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }


        /// <summary>
        /// email功能 不可用 azure
        /// </summary>
        /// <param name="cartDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
            });
        }

    }
}
