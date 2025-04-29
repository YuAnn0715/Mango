using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        /// <summary>
        /// 異步登入
        /// </summary>
        /// <param name="loginRequestDto">登入請求</param>
        /// <returns>登入結果</returns>
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// 寄存資訊
        /// </summary>
        /// <param name="registrationRequestDto">註冊請求</param>
        /// <returns></returns>
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// 角色設定
        /// </summary>
        /// <param name="registrationRequestDto">註冊請求</param>
        /// <returns></returns>
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
