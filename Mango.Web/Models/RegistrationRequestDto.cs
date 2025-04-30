using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // 註冊請求view
    public class RegistrationRequestDto
    {
        // 電子信箱
        [Required]
        public string Email { get; set; }
        
        // 姓名
        [Required]
        public string Name { get; set; }

        // 手機號碼
        [Required]
        public string PhoneNumber { get; set; }

        // 密碼
        [Required]
        public string Password { get; set; }

        // 角色
        public string? Role { get; set; }
    }
}
