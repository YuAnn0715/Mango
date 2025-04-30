namespace Mango.Web.Models
{
    // 購物車詳細內容
    public class CartDetailsDto
    {
        // 購物車詳細內容Id
        public int CartDetailsId { get; set; }

        // 購物車Id
        public int CartHeaderId { get; set; }

        // 購物車內容
        public CartHeaderDto? CartHeader { get; set; }

        // 產品Id
        public int ProductId { get; set; }

        // 產品內容
        public ProductDto? Product { get; set; }

        // 產品數量
        public int Count { get; set; }
    }
}
