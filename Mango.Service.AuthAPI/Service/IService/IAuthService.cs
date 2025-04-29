using Mango.Service.AuthAPI.Models.Dto;
using Microsoft.Win32;

namespace Mango.Service.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="registrationRequestDto">註冊資訊</param>
        /// <returns>註冊結果字串</returns>
        Task<string> Register(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginRequestDto">頁面登入資訊</param>
        /// <returns>登入回應</returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        /// <summary>
        ///  角色設定
        /// </summary>
        /// <param name="email">使用者email</param>
        /// <param name="roleName">角色名稱</param>
        /// <returns>是否</returns>
        Task<bool> AssignRole(string email,string roleName);
    }
}
