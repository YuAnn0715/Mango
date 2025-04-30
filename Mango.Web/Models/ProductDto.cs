using Mango.Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // 產品view
    public class ProductDto
    {
        // 產品編號
        public int ProductId { get; set; }

        // 產品名稱
        public string Name { get; set; }

        // 產品價格
        public double Price { get; set; }

        // 產品描述
        public string Description { get; set; }

        // 產品類別名稱
        public string CategoryName { get; set; }

        // 產品圖片Url
        public string? ImageUrl { get; set; }

        // 產品圖片本地路徑
        public string? ImageLocalPath { get; set; }

        // 產品總數
        [Range(1, 100)]
        public int Count { get; set; } = 1;

        // Utility 驗證擴充(可自訂)
        [MaxFileSize(1)]
        [AllowedExtensions(new string[] { ".jpg",".png"})]
        // 上傳的圖檔
        public IFormFile? Image { get; set; }

	}
}
