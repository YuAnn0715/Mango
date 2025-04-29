using Mango.Service.AuthAPI.Migrations;
using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Service.AuthAPI.Service
{
    /// <summary>
    /// 生成JWT安全令牌
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        // DI注入
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser applicationUser,IEnumerable<string>roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // 提取金鑰
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            // 要求清單
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName)
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // 令牌屬性描述
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                // 內容
                Subject = new ClaimsIdentity(claimList),
                // 日期期限
                Expires=DateTime.UtcNow.AddDays(7),
                // 簽名憑證(HmacSha256Signature 演算法加密)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
