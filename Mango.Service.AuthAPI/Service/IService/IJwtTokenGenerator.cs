using Mango.Service.AuthAPI.Migrations;

namespace Mango.Service.AuthAPI.Service.IService
{
    /// <summary>
    ///  生成JWT安全令牌
    /// </summary>
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string>roles);
    }
}
