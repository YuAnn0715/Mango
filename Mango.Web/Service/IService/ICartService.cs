using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        // 依userId取得對應購物車
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);

        // 新增更改購物車
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);

        // 刪除購物車內容
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);

        // 使用優惠券
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);

        // email 不可用
        Task<ResponseDto?> EmailCart(CartDto cartDto);
    }
}
