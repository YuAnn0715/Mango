using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    // 優惠券
    public class Coupon
    {
        // Id
        [Key] //主key
        public int CouponId { get; set; }

        // 優惠券代碼
        [Required]  //不可為null
        public string CouponCode { get; set; }

        // 折扣金額
        [Required]
        public double DiscountAmount { get; set; }

        // 最小可用折扣金額
        public int MinAmount { get; set; }

    }
}
