namespace Mango.Services.ProductAPI.Models.Dto
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

        // 產品圖片路徑
        public string? ImageUrl { get; set; }

		// 產品圖片本地路徑
		public string? ImageLocalPath { get; set; }

        // 產品圖片 Web 傳遞上傳
        public IFormFile? Image { get; set; }
    }
}
