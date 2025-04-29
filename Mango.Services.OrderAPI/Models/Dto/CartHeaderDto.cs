
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Models.Dto
{
    public class CartHeaderDto
    {
        // 購物車Id
        public int CartHeaderId { get; set; }

        // 使用者Id
        public string? UserId { get; set; }

        // 優惠券代碼
        public string? CouponCode { get; set; }

        // 折扣數
        public double Discount { get; set; }

        // 購物車總計
        public double CartTotal { get; set; }

        // 使用者資料
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}