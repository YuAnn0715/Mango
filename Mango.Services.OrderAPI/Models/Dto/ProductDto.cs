namespace Mango.Services.OrderAPI.Models.Dto
{
    public class ProductDto
    {
        // 產品Id
        public int ProductId { get; set; }

        // 產品名稱
        public string Name { get; set; }

        // 產品價格
        public double Price { get; set; }

        // 產品描述
        public string Description { get; set; }

        // 產品類別名稱
        public string CategoryName { get; set; }

        // 產品圖片連結
        public string? ImageUrl { get; set; }

        // 產品數量
        public int Count { get; set; } = 1;
    }
}