using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.ProductAPI.Controllers
{
	[Route("api/product")]
	[ApiController]
	//[Authorize]
	public class ProductAPIController : ControllerBase
	{
		//DI注入
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;

		public ProductAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDto();
		}

		/// <summary>
		/// 取得全部產品
		/// </summary>
		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Product> objList = _db.Products.ToList();
				_response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		/// <summary>
		/// 取得指定產品
		/// </summary>
		/// <param name="id">產品Id</param>
		[HttpGet]
		[Route("{id:int}")]  //在相同Get名稱方法下 要有路由設定 Swagger才能分辨兩個端點之間的差異
		public ResponseDto Get(int id)
		{
			try
			{
				Product obj = _db.Products.First(u => u.ProductId == id);
				_response.Result = _mapper.Map<ProductDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		/// <summary>
		/// 新增產品
		/// </summary>
		/// <param name="productDto">產品請求內容</param>
		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Post(ProductDto productDto)
		{
			try
			{
				Product product = _mapper.Map<Product>(productDto);
				_db.Products.Add(product);
				_db.SaveChanges();

				if (productDto.Image != null)
				{
					// 圖檔名
					string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
					// 圖檔複製存放路徑
					string fillePath = @"wwwroot\ProductImages\" + fileName;
					var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), fillePath);
					using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
					{
                        productDto.Image.CopyTo(fileStream);
					}

					// 圖檔完整路徑
					var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
					product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
					product.ImageLocalPath = fillePath;
				}
				else
				{
					product.ImageUrl = "https://placehold.co/600x400";
				}

				// 更新產品
				_db.Products.Update(product);
				_db.SaveChanges();
				_response.Result = _mapper.Map<ProductDto>(product);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		/// <summary>
		/// 更新產品
		/// </summary>
		/// <param name="productDto">產品請求內容</param>
		[HttpPut]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Put(ProductDto productDto)
		{
			try
			{
				Product product = _mapper.Map<Product>(productDto);

                if (productDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        // 目錄位置
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    // 圖檔名
                    string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                    // 圖檔複製存放路徑
                    string fillePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), fillePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productDto.Image.CopyTo(fileStream);
                    }

                    // 圖檔完整路徑
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = fillePath;
                }

                _db.Products.Update(product);
				_db.SaveChanges();
				_response.Result = _mapper.Map<ProductDto>(product);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		/// <summary>
		/// 刪除指定產品
		/// </summary>
		/// <param name="id">產品Id</param>
		[HttpDelete]
		[Route("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Delete(int id)
		{
			try
			{
				Product obj = _db.Products.First(u => u.ProductId == id);
				
				if (!string.IsNullOrEmpty(obj.ImageLocalPath))
				{
					// 目錄位置
					var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
					FileInfo file = new FileInfo(oldFilePathDirectory);
					if (file.Exists)
					{
						file.Delete();
					}
				}

				_db.Products.Remove(obj);
				_db.SaveChanges();
				_response.Result = _mapper.Map<ProductDto>(obj);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}
