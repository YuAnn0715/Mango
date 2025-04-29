namespace Mango.Services.CouponAPI.Models.Dto
{
    // 請求回應的優惠券內容
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
