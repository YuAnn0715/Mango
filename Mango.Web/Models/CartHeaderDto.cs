using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // 購物車view
    public class CartHeaderDto
    {
        // 購物車Id
        public int CartHeaderId { get; set; }

        // 使用者Id
        public string? UserId { get; set; }

        // 優惠券代碼
        public string? CouponCode { get; set; }

        // 優惠券折扣金額
        public double Discount { get; set; }

        // 購物車總金額
        public double CartTotal { get; set; }

        /// <summary>
        /// email
        /// </summary>
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public string? LastName { get; set; }

        // 訂單登記的人名
        [Required]
        public string? Name { get; set; }

        // 訂單登記的手機號碼
        [Required]
        public string? Phone { get; set; }

        // 訂單登記的電子信箱
        [Required]
        public string? Email { get; set; }
    }
}
