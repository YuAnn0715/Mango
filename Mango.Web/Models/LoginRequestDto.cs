using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // 使用者登入請求view
    public class LoginRequestDto
    {
        // 登入帳號
        [Required]
        public string UserName { get; set; }

        // 登入密碼
        [Required]
        public string Password { get; set; }
    }
}
