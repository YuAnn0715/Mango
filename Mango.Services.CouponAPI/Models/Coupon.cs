using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    // 優惠券資料庫欄位名稱
    public class Coupon
    {
        [Key] //主key
        public int CouponId { get; set; }
        [Required]  //不可為null
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }

    }
}
