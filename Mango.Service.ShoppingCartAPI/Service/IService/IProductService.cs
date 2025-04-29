using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        // 取得產品
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
