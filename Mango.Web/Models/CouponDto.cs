namespace Mango.Web.Models
{
    // 優惠券view
    public class CouponDto
    {
        // 優惠券Id
        public int CouponId { get; set; }

        // 優惠券代碼
        public string CouponCode { get; set; }

        // 優惠券折扣金額
        public double DiscountAmount { get; set; }

        // 可使用此優惠券券的最小金額
        public int MinAmount { get; set; }
    }
}
