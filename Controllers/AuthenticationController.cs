using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private static UserLoginDto? _user;
        private readonly DataContext context;
        private readonly IAuthenticationRepository authenticationRepository;
        public AuthenticationController(DataContext context, IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("fetch/user/{token}")]
        public async Task<IActionResult> Fetch(string token)
        {
            // string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.ReadJwtToken(token);

            int userId = Int32.Parse(readToken.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            var user = await authenticationRepository.GetUser(userId);

            user.UserToken = token;

            return Ok(user);
        }

        [Authorize(Roles = "Customer, Admin, Manager, Staff")]
        [HttpPost("refresh-token")]
        public ActionResult<string> Refresh()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
            if (!_user!.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh-token.");
            }
            else if (_user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            var token = authenticationRepository.RecreateToken(_user);

            _user.UserToken = token;

            return Ok(_user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDto>> Login(LoginDto request)
        {
            var user = await authenticationRepository.CheckLogin(request);

            var newRefreshToken = authenticationRepository.RefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken!.Expires,
            };
            Response.Cookies.Append("refreshtoken", newRefreshToken.Token, cookieOptions);

            user!.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            _user = user;

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("verify-access-token/{access_token}")]
        public async Task<IActionResult> VerifyAccessToken(string access_token)
        {
            var result = await authenticationRepository.AuthenFirebase(access_token);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("send-otp/{recipientPhone}")]
        public async Task<IActionResult> SendOTP(string recipientPhone)
        {
            await authenticationRepository.SendOTP(recipientPhone);
            throw new MyException("Successfully.", 200);
        }

        [AllowAnonymous]
        [HttpPost("validate-otp/{otp}&{recipientPhone}")]
        public async Task<IActionResult> ValidateOTP(string otp, string recipientPhone)
        {
            await authenticationRepository.ValidateOTP(otp, recipientPhone);
            throw new MyException("Successfully.", 200);
        }

        [AllowAnonymous]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto requestDto)
        {
            await authenticationRepository.ChangePassword(requestDto);
            throw new MyException("Successfully.", 200);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterRequestDto requestDto)
        {
            await authenticationRepository.UserRegister(requestDto);
            throw new MyException("Successfully.", 200);
        }


        [AllowAnonymous]
        [HttpGet("check-email-exist/{recipientEmail}")]
        public async Task<IActionResult> CheckEmailExist(string recipientEmail)
        {
            var email = await authenticationRepository.IsEmailExist(recipientEmail);
            return Ok(email);
        }

        [AllowAnonymous]
        [HttpGet("count/{entity}")]
        public async Task<IActionResult> Count(string entity)
        {
            var count = await authenticationRepository.Count(entity);
            return Ok(count);
        }
    }
}