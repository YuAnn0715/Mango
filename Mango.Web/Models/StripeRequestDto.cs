namespace Mango.Web.Models
{
    // Stripe 支付請求view
    public class StripeRequestDto
    {
        // Stripe 支付 Session Url
        public string? StripeSessionUrl { get; set; }

        // Stripe 支付 Session Id
        public string? StripeSessionId { get; set; }

        // Stripe 支付 核准 Url
        public string ApprovedUrl { get; set; }

        // Stripe 支付 取消 Url
        public string CancelUrl { get; set; }

        // 訂單內容到頁面
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
