namespace Mango.Web.Models
{
    // 訂單view
    public class OrderHeaderDto
    {
        // 訂單Id
        public int OrderHeaderId { get; set; }

        // 使用者Id
        public string? UserId { get; set; }

        // 優惠券代碼
        public string? CouponCode { get; set; }

        // 優惠券折扣金額
        public double Discount { get; set; }

        // 訂單總金額
        public double OrderTotal { get; set; }

        // 訂單登記的人名
        public string? Name { get; set; }

        // 訂單登記的手機號碼

        public string? Phone { get; set; }

        // 訂單登記的電子信箱

        public string? Email { get; set; }

        // 訂單時間
        public DateTime OrderTime { get; set; }

        // 訂單狀態
        public string? Status { get; set; }

        // 支付Id
        public string? PaymentIntentId { get; set; }

        // Stripe 支付產生的 Session Id 
        public string? StripeSessionId { get; set; }

        // 訂單內容
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}