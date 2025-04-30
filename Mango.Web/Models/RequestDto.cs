using static Mango.Web.Utility.SD;

namespace Mango.Web.Models
{
    // 請求view
    public class RequestDto
    {
        // API類型 (預設為 GET)
        public ApiType ApiType { get; set; } = ApiType.GET;

        // API Url
        public string Url { get; set; }

        // 傳遞的資料
        public object Data { get; set; }

        // 訪問的token(身份驗證)
        public string AccessToken { get; set; }

		// 傳遞內容資料類型 (預設為 Json)
		public ContentType ContentType { get; set; } = ContentType.Json;

	}
}
