using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        // DI注入
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<BaseService> _logger;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider, ILogger<BaseService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }

        /// <summary>
        /// API回應
        /// </summary>
        /// <param name="requestDto">API請求</param>
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                _logger.LogInformation("開始處理API請求: {Url}, 方法: {ApiType}", requestDto.Url, requestDto.ApiType);
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                // 檢查傳遞資料的類別
                // 圖檔
                if (requestDto.ContentType == ContentType.MultipartFormData)
                {
					//  */* => 任意媒體類型
					message.Headers.Add("Accept", "*/*");
				}
                else
                {
					message.Headers.Add("Accept", "application/json");
				}
				// token
				if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    if (string.IsNullOrEmpty(token))
                    {
                        _logger.LogWarning("Bearer token is null or empty.");
                    }
                    else
                    {
                        message.Headers.Add("Authorization", $"Bearer {token}");
                        _logger.LogInformation("Attach Bearer Token: {Token}", token.Substring(0, 5) + "...");
                    }
                }
                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.ContentType == ContentType.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();
                    foreach (var prop in requestDto.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(requestDto.Data);
                        if (value is FormFile file)
                        {
                            if (file != null && file.Length > 0)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                            else
                            {
                                _logger.LogWarning("檔案: {FileName} is null or empty.", file?.FileName);
                            }
                        }
                        else
                        {
                            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else 
                {
                    if (requestDto.Data != null)
                    {
						message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
					}
                }
                HttpResponseMessage? apiResponse = null;

                message.Method = requestDto.ApiType switch
                {
                    // 陳述式switch
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    ApiType.GET => HttpMethod.Get,
                    _ => HttpMethod.Get,
                };
                _logger.LogInformation("寄送 HTTP 請求: {Method} {Url}", message.Method, message.RequestUri);

                apiResponse = await client.SendAsync(message);
                _logger.LogInformation("HTTP 回應: {StatusCode}", apiResponse.StatusCode);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "未找到(Not Found)" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "存取遭拒(Access Denied)" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "未授權(Unauthorized)" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "內部伺服器錯誤(Internal Server Error)" };
                    default:
                        if (apiResponse.IsSuccessStatusCode)
                        {
                            var apiContent = await apiResponse.Content.ReadAsStringAsync();
                            try
                            {
                                var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                                return apiResponseDto;
                            }
                            catch (JsonException ex)
                            {
                                _logger.LogError(ex, "API 回應反序列化失敗.");
                                return new ResponseDto
                                {
                                    IsSuccess = false,
                                    Message = "無效的 API 回應格式"
                                };
                            }
                        }
                        else
                        {
                            return new ResponseDto
                            {
                                IsSuccess = false,
                                Message = $"意外的錯誤的狀態代碼: {apiResponse.StatusCode}"
                            };
                        }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}