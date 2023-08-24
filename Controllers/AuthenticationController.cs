using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private static UserLoginDto? _user;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly IUserRepository userRepository;
        public AuthenticationController(IAuthenticationRepository authenticationRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Fetch user information.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("fetch/user/{token}")]
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

        /// <summary>
        /// Re-create a new token for a user if their token expired. [Web]
        /// </summary>
        [Authorize(Roles = "RefreshToken")]
        [HttpGet("refresh-token-web")]
        public IActionResult RefreshWeb()
        {
            var refreshToken = Request.Cookies["refreshtoken"];

            if (!_user!.RefreshToken.Equals(refreshToken))
            {
                throw new MyException("Invalid refresh-token.", 401);
            }
            else if (_user.RefreshTokenExpires < DateTime.Now)
            {
                throw new MyException("Token expired.", 401);
            }

            var token = authenticationRepository.RecreateToken(_user);

            _user.UserToken = token;

            return Ok(_user);
        }

        /// <summary>
        /// Re-create a new token for a user if their token expired. [Mobile]
        /// </summary>
        [Authorize(Roles = "RefreshToken")]
        [HttpGet("refresh-token-mobile")]
        public IActionResult RefreshMobile()
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = int.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            var user = userRepository.Detail(userId).Result;

            switch (_user is null)
            {
                case true:
                    if (user!.RefreshToken is null)
                    {
                        throw new MyException("refresh-token not exists.", 401);
                    }
                    else if (user.RefreshTokenExpires < DateTime.Now)
                    {
                        throw new MyException("Token expired.", 401);
                    }

                    var loginDto = new UserLoginDto();

                    loginDto!.UserId = user.UserId;
                    loginDto.GarageId = 0;
                    loginDto.UserFirstName = user.UserFirstName;
                    loginDto.UserLastName = user.UserLastName;
                    loginDto.UserFullName = user.UserFirstName + " " + user.UserLastName;
                    loginDto.UserImage = user.UserImage;
                    loginDto.UserPhone = "1";
                    loginDto.RefreshToken = user.RefreshToken;
                    loginDto.RefreshTokenCreated = user.RefreshTokenCreated;
                    loginDto.RefreshTokenExpires = user.RefreshTokenExpires;
                    loginDto.RoleDto = new RoleDto(){RoleName = user.Role.RoleName};

                    loginDto.UserToken = authenticationRepository.RecreateToken(loginDto);

                    return Ok(loginDto);
                case false:
                    if (!_user!.RefreshToken.Equals(user!.RefreshToken))
                    {
                        throw new MyException("Invalid refresh-token.", 401);
                    }
                    else if (user.RefreshTokenExpires < DateTime.Now)
                    {
                        throw new MyException("Token expired.", 401);
                    }

                    _user.UserToken = authenticationRepository.RecreateToken(_user);

                    return Ok(_user);
            }

            // var newRefreshToken = authenticationRepository.RefreshToken(userId);

            // var refreshToken = newRefreshToken.Result!.Token;
            // var refreshTokenCreated = newRefreshToken.Result!.Created;
            // var refreshTokenExpires = newRefreshToken.Result!.Expires;

            // user.RefreshToken = refreshToken;
            // user.RefreshTokenCreated = refreshTokenCreated;
            // user.RefreshTokenExpires = refreshTokenExpires;

            // userRepository.Update(user);

            // _user.RefreshToken = refreshToken;
            // _user.RefreshTokenCreated = refreshTokenCreated;
            // _user.RefreshTokenExpires = refreshTokenExpires;
        }

        /// <summary>
        /// Login for user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST Login for customer and mechanic.
        ///     {
        ///        "phone": "phone",
        ///        "password": "password"
        ///     }
        ///
        ///     POST Login for admin and manager.
        ///     {
        ///        "email": "email",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDto>> Login(LoginDto request)
        {
            var user = await authenticationRepository.CheckLogin(request);

            var newRefreshToken = await authenticationRepository.RefreshToken(user!.UserId);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken!.Expires,
            };

            Response.Cookies.Append("refreshtoken", newRefreshToken.Token, cookieOptions);

            user!.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenCreated = newRefreshToken.Created;
            user.RefreshTokenExpires = newRefreshToken.Expires;

            _user = user;

            return Ok(user);
        }

        // <summary>
        // Verify access token from Firebase when using login with Google.
        // </summary>
        // [AllowAnonymous]
        // [HttpGet("verify-access-token/{access_token}")]
        // public async Task<IActionResult> VerifyAccessToken(string access_token)
        // {
        //     var result = await authenticationRepository.AuthenFirebase(access_token);
        //     return Ok(result);
        // }

        /// <summary>
        /// Send OTP through sms when user register.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("send-otp/{recipientPhone}")]
        public async Task<IActionResult> SendOTP(string recipientPhone)
        {
            await authenticationRepository.SendOTP(recipientPhone);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Validate OTP.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("validate-otp/{otp}&{recipientPhone}")]
        public async Task<IActionResult> ValidateOTP(string otp, string recipientPhone)
        {
            await authenticationRepository.ValidateOTP(otp, recipientPhone);
            throw new MyException("Thành công.", 200);
        }

        // /// <summary>
        // /// Change the password if a user forgot.
        // /// </summary>
        // [AllowAnonymous]
        // [HttpPost("change-password")]
        // public async Task<IActionResult> ChangePassword(ChangePasswordDto requestDto)
        // {
        //     await authenticationRepository.ChangePassword(requestDto);
        //     throw new MyException("Thành công.", 200);
        // }

        /// <summary>
        /// Register new users.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterRequestDto requestDto)
        {
            await authenticationRepository.UserRegister(requestDto);
            throw new MyException("Thành công.", 200);
        }

        // /// <summary>
        // /// check if users are avaliable in data or not.
        // /// </summary>
        // [AllowAnonymous]
        // [HttpGet("check-email-exist/{recipientEmail}")]
        // public async Task<IActionResult> CheckEmailExist(string recipientEmail)
        // {
        //     var email = await authenticationRepository.IsEmailExist(recipientEmail);
        //     return Ok(email);
        // }

        [AllowAnonymous]
        [HttpGet("count/{entity}")]
        public async Task<IActionResult> Count(string entity)
        {
            var count = await authenticationRepository.Count(entity);
            return Ok(count);
        }
    }
}