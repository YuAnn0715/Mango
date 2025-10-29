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
                _logger.LogInformation("Start processing API requests: {Url}, Function: {ApiType}", requestDto.Url, requestDto.ApiType);
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
                        if (value is FormFile)
                        {
                            var file = (FormFile)value;
                            if (file != null && file.Length > 0)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                            else
                            {
                                _logger.LogWarning("File {FileName} is null or empty.", file?.FileName);
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

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                _logger.LogInformation("Sending HTTP requests: {Method} {Url}", message.Method, message.RequestUri);

                apiResponse = await client.SendAsync(message);
                _logger.LogInformation("HTTP response received: {StatusCode}", apiResponse.StatusCode);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
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
                                _logger.LogError(ex, "Failed to deserialize API response.");
                                return new ResponseDto
                                {
                                    IsSuccess = false,
                                    Message = "Invalid response format."
                                };
                            }
                        }
                        else
                        {
                            return new ResponseDto
                            {
                                IsSuccess = false,
                                Message = $"Unexpected status code: {apiResponse.StatusCode}"
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