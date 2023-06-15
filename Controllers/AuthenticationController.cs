using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO.Exception;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static UserLoginDto? user;
        private readonly DataContext context;
        private readonly IAuthenticationRepository authenticationRepository;
        public AuthenticationController(DataContext context, IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
            this.context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDto>> Login(LoginDto request)
        {
            var _user = await authenticationRepository.CheckLogin(request);
            user = _user;
            return Ok(user);
        }

        [HttpPost("refresh-token")]
        public ActionResult<string> Refresh()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
            if (user!.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            var newRefreshToken = authenticationRepository.RefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken!.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            return Ok(user);
        }

        [HttpPost("verify-access-token/{access_token}")]
        public async Task<IActionResult> VerifyAccessToken(string access_token)
        {
            var result = await authenticationRepository.AuthenFirebase(access_token);
            return Ok(result);
        }

        [HttpPost("send-otp/{recipientEmail}")]
        public async Task<IActionResult> SendOTP(string recipientEmail)
        {
            await authenticationRepository.SendOTP(recipientEmail);
            throw new MyException("Successfully.", 200);
        }

        [HttpPost("validate-otp/{otp}")]
        public async Task<IActionResult> ValidateOTP(string otp){
            if(authenticationRepository.ValidateOTP(otp)){
                throw new MyException("Successfully.", 200);
            }
            throw new MyException("Wrong OTP.", 404);
        }

        // [HttpPost("register")]
        // public async Task<ActionResult> Register(CreateUserDto request)
        // {
        //     try
        //     {
        //         await authenticationRepository.CreateUser(request);
        //         return Ok(context.Users.FirstOrDefault(u => u.UserEmail == request.user_email));
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}