using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IOrderService
    {
        // 建立訂單
        Task<ResponseDto?> CreateOrder(CartDto cartDto);

        // 建立 Stripe 支付 Session
        Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);

        // 檢查訂單
        Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);

        // 取得全部訂單
        Task<ResponseDto?> GetAllOrder(string? userId);

        // 取的指定訂單
        Task<ResponseDto?> GetOrder(int orderId);

        // 更新訂單狀態
        Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
    }
}