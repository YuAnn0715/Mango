namespace Mango.Web.Models
{
    // API回應結果
    public class ResponseDto
    {
        // 回應資料
        public object? Result { get; set; }

        // 是否成功
        public bool IsSuccess { get; set; } = true;

        // 錯誤訊息
        public string Message { get; set; } = "";
    }
}
