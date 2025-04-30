using Mango.Web.Models;
using Mango.Web.Service;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {   // DI注入
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService, ICartService cartService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _cartService = cartService;
            _logger = logger;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = [];
            ResponseDto? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                //回應不成功存取錯誤臨時數據
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        /// <summary>
        /// 商品詳細
        /// </summary>
        /// <param name="productId">產品編號</param>
        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? model = new();
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                //回應不成功存取錯誤臨時數據
                TempData["error"] = response?.Message;
            }
            return View(model);
        }

        /// <summary>
        /// 商品詳細
        /// </summary>
        /// <param name="productDto">產品請求內容</param>
        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            // 建立新購物車
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };
            // 產品內容數量填充
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };
            cartDto.CartDetails = cartDetailsDtos;

            ResponseDto? response = await _cartService.UpsertCartAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item has been added to the Shopping Cart!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //回應不成功存取錯誤臨時數據
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }
    }
}
