using Mango.Service.AuthAPI.Migrations;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {

        //DI注入
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService athService, ITokenProvider tokenProvider)
        {
            _authService = athService;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        /// <summary>
        /// 資料庫登入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);
            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                // 調用使用者請求給頁面
                await SignInUser(loginResponseDto);

                // 存取token到cookies
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {

                TempData["error"] = responseDto.Message;
                return View(obj);
            }

            var roleList = new List<SelectListItem>()
            {
              new SelectListItem{ Text=SD.RoleAdmin,Value=SD.RoleAdmin},
              new SelectListItem{ Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.RoleLsit = roleList;
            return View(obj);
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
              new SelectListItem{ Text=SD.RoleAdmin,Value=SD.RoleAdmin},
              new SelectListItem{ Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };

            ViewBag.RoleLsit = roleList;
            return View();
        }

        /// <summary>
        /// 註冊到資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assingRple;
            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assingRple = await _authService.AssignRoleAsync(obj);
                if (assingRple != null && assingRple.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
              new SelectListItem{ Text=SD.RoleAdmin,Value=SD.RoleAdmin},
              new SelectListItem{ Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.RoleLsit = roleList;
            return View(obj);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            // 清除cookies
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 調用使用者請求給頁面
        /// </summary>
        /// <returns></returns>
        private async Task SignInUser(LoginResponseDto model)
        {
            // 用來解析 JWT Token
            var handler = new JwtSecurityTokenHandler();

            // Token 字串解析成 JwtSecurityToken 物件
            var jwt = handler.ReadJwtToken(model.Token);

            // 填入身份
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


            // ClaimsIdentity 包裝成 ClaimsPrincipal 使用者物件
            var principal = new ClaimsPrincipal(identity);

            // 完成登入的動作，將使用者資訊存入 Cookie 中。之後用戶就會被視為已登入
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
