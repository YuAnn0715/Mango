using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    // 產品
    public class Product
    {
        // 產品Id
        [Key]
        public int ProductId { get; set; }
        [Required]

        // 產品名稱
        public string Name { get; set; }

        // 產品價格
        [Range(1, 1000)]
        public double Price { get; set; }

        // 產品描述
        public string Description { get; set; }

        // 產品類別名稱
        public string CategoryName { get; set; }

        // 產品圖片路徑
        public string? ImageUrl { get; set; }

        // 產品圖片本地路徑
        public string? ImageLocalPath { get; set; }
    }
}
