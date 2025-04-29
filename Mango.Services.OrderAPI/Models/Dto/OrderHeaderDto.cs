
namespace Mango.Services.OrderAPI.Models.Dto
{
    public class OrderHeaderDto
    {
        // 訂單Id
        public int OrderHeaderId { get; set; }

        // 使用者Id
        public string? UserId { get; set; }

        // 優惠券代碼
        public string? CouponCode { get; set; }

        // 折扣價
        public double Discount { get; set; }

        // 訂單總金額
        public double OrderTotal { get; set; }

        // 使用者資訊
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        // 訂單時間
        public DateTime OrderTime { get; set; }

        // 訂單狀態
        public string? Status { get; set; }

        // 支付Id
        public string? PaymentIntentId { get; set; }

        // 
        public string? StripeSessionId { get; set; }

        // 訂單內容
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}