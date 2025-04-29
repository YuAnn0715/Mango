using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        //DI注入
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 產品首頁
        /// </summary>
        public async Task<IActionResult> ProductIndex()
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
        /// 新增產品頁面
        /// </summary>
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        /// <summary>
        /// 調用新增產品API
        /// </summary>
        /// <param name="model">產品內容</param>
        /// <returns>結果</returns>
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductsAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully!";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        /// <summary>
        /// 編輯產品頁面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        /// <summary>
        /// 調用編輯產品API
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            ResponseDto? response = await _productService.UpdateProductsAsync(productDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }


        /// <summary>
        /// 刪除產品頁面
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        /// <summary>
        /// 調用刪除產品API
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? response = await _productService.DeleteProductsAsync(productDto.ProductId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully!";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }
    }
}
