namespace Mango.Service.AuthAPI.Models
{
    // Jwt令牌
    public class JwtOptions
    {
        // 密令
        public string Secret { get; set; } = string.Empty;

        // 發行者
        public string Issuer { get; set; } = string.Empty;

        // 讀者
        public string Audience { get; set; } = string.Empty;
    }
}
