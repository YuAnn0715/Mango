namespace Mango.Web.Models
{
    // 購物車view
    public class CartDto
    {
        // 購物車內容
        public CartHeaderDto CartHeader { get; set; }

        // 購物車詳細內容
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}