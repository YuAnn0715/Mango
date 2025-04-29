namespace Mango.Service.AuthAPI.Models.Dto
{
    // 使用者登入請求
    public class LoginRequestDto
    {
        // 登入帳號
        public string UserName { get; set; }

        // 登入密碼
        public string Password { get; set; }
    }
}
