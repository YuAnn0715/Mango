
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.OrderAPI.Models.Dto
{
    public class CartDetailsDto
    {
        // 購物車詳細Id
        public int CartDetailsId { get; set; }

        // 購物車標題Id
        public int CartHeaderId { get; set; }

        // 購物車
        public CartHeaderDto? CartHeader { get; set; }

        // 產品Id
        public int ProductId { get; set; }

        // 產品
        public ProductDto Product { get; set; }

        // 產品數量
        public int Count { get; set; }
    }
}