namespace Mango.Web.Models
{
    // 使用者登入回應view
    public class LoginResponseDto
    {
        // 使用者資料
        public UserDto User { get; set; }

        // JWT安全令牌
        public string Token { get; set; }
    }
}
