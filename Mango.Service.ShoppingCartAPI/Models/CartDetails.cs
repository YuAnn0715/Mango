using Mango.Services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        // 購物車詳細Id
        [Key]
        public int CartDetailsId { get; set; }

        // 購物車標題Id
        public int CartHeaderId { get; set; }

        // 購物車標題
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        // 產品Id
        public int ProductId { get; set; }

        // 產品
        [NotMapped] //不儲存到資料庫(不印射)
        public ProductDto Product { get; set; }

        // 產品數量
        public int Count { get; set; }

    }
}