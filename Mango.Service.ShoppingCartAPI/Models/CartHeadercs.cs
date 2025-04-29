using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        // 購物車Id
        [Key]
        public int CartHeaderId { get; set; }

        // 使用者Id
        public string? UserId { get; set; }

        // 優惠券代碼
        public string? CouponCode { get; set; }

        // 折扣數
        [NotMapped] //不儲存到資料庫(不印射)
        public double Discount { get; set; }

        // 購物車總計
        [NotMapped] //不儲存到資料庫(不印射)
        public double CartTotal { get; set; }
    }
}