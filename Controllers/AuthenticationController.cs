using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static UserDto? user;
        private readonly DataContext context;
        private readonly IAuthenticationRepository authenticationRepository;
        public AuthenticationController(DataContext context, IAuthenticationRepository authenticationRepository){
            this.authenticationRepository = authenticationRepository;
            this.context = context;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            try
            {
                var _user = await authenticationRepository.CheckLogin(request);
                user = _user;
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public ActionResult<string> Refresh()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshtoken"];
                if (user!.refresh_token.Equals(refreshToken))
                {
                    return Unauthorized("Invalid refresh token.");
                }
                else if (user.token_expires < DateTime.Now)
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

                user.refresh_token = newRefreshToken.Token;
                user.token_created = newRefreshToken.Created;
                user.token_expires = newRefreshToken.Expires;

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("verify-access-token/{access-token}")]
        public async Task<IActionResult> VerifyAccessToken(string request)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDto request)
        {
            try
            {
                await authenticationRepository.CreateUser(request);
                return Ok(context.Users.FirstOrDefault(u => u.user_email == request.user_email));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}