namespace Mango.Service.AuthAPI.Models.Dto
{
    // 註冊請求
    public class RegistrationRequestDto
    {
        // 電子信箱
        public string Email { get; set; }

        // 姓名
        public string Name { get; set; }

        // 電話號碼
        public string PhoneNumber { get; set; }

        // 密碼
        public string Password { get; set; }

        // 角色
        public string? Role { get; set; }
    }
}
