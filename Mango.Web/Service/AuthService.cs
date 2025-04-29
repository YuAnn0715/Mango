using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class AuthService: IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// 角色設定
        /// </summary>
        /// <param name="registrationRequestDto">註冊請求</param>
        /// <returns></returns>
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto) 
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }


        /// <summary>
        /// 異步登入
        /// </summary>
        /// <param name="loginRequestDto">登入請求</param>
        /// <returns>登入結果</returns>
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }


        /// <summary>
        /// 寄存資訊
        /// </summary>
        /// <param name="registrationRequestDto">註冊請求</param>
        /// <returns></returns>
        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
