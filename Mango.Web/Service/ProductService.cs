using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// 取得全部優惠券
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        /// <summary>
        /// 依Id取得產品
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }

        /// <summary>
        /// 建立新產品
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product",
                ContentType = SD.ContentType.MultipartFormData
            });
        }

        /// <summary>
        /// 更新產品
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product",
				ContentType = SD.ContentType.MultipartFormData
			});
        }

        /// <summary>
        /// 刪除產品
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public async Task<ResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }
    }
}
