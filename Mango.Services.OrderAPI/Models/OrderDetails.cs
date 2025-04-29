using Mango.Services.OrderAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Models
{
    public class OrderDetails
    {
        // 訂單內容細節Id
        [Key]
        public int OrderDetailsId { get; set; }

        // 訂單Id
        public int OrderHeaderId { get; set; }

        // 訂單內容
        [ForeignKey("OrderHeaderId")]
        public OrderHeader? OrderHeader { get; set; }

        // 產品Id
        public int ProductId { get; set; }

        // 產品內容
        [NotMapped] // EF CORE 不映射
        public ProductDto? Product { get; set; }

        // 產品數量
        public int Count { get; set; }

        // 產品名稱
        public string ProductName { get; set; }

        // 產品價格
        public double Price { get; set; }
    }
}