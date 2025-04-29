using Mango.Services.OrderAPI.Models.Dto;

namespace Mango.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        // 取得產品
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
