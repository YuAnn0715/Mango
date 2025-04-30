namespace Mango.Web.Models
{
    // 訂單詳細view
    public class OrderDetailsDto
    {
        // 訂單內容Id
        public int OrderDetailsId { get; set; }

        // 訂單Id
        public int OrderHeaderId { get; set; }

        // 產品Id
        public int ProductId { get; set; }

        // 產品內容
        public ProductDto? Product { get; set; }

        // 產品數量
        public int Count { get; set; }

        // 產品名稱
        public string ProductName { get; set; }

        // 產品價格
        public double Price { get; set; }
    }
}