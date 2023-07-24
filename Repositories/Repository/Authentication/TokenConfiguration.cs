using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using Microsoft.IdentityModel.Tokens;
using GraduationThesis_CarServices.Models.Entity;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class TokenConfiguration
    {
        private readonly IConfiguration _configuration;

        public TokenConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(UserLoginDto user)
        {
            try
            {
                List<Claim> claims = new List<Claim>{
                    new Claim("userId", user.UserId.ToString()),
                    new Claim("garageId", user.GarageId.ToString()),
                    new Claim("firstname", user.UserFirstName! is not null ? user.UserFirstName : ""),
                    new Claim("lastname", user.UserLastName! is not null ? user.UserLastName : ""),
                    new Claim("name", user.UserFirstName + user.UserLastName),
                    new Claim("phone", user.UserPhone),
                    new Claim("image", user.UserImage! is not null ? user.UserImage : ""),
                    new Claim(ClaimTypes.Role, user.RoleDto!.RoleName)};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Key"],
                    audience: _configuration["Jwt:Key"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: cred
                );

                var from = token.ValidFrom;
                var to = token.ValidTo;

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public RefreshTokenDto GenerateRefreshToken(int userId)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("userId", userId.ToString()),
                    new Claim(ClaimTypes.Role, "RefreshToken")};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var expired = DateTime.Now.AddHours(7);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expired,
                    signingCredentials: cred
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                var refreshToken = new RefreshTokenDto
                {
                    Token = jwt,
                    //Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expires = expired,
                    Created = DateTime.Now
                };

                return refreshToken;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public string ReCreateFirebaseToken(User user, string uid)
        {
            try
            {
                if (user.UserEmail != null)
                {
                    List<Claim> claims = new List<Claim>{
                //new Claim(ClaimTypes.Name, account.Owner),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Uri, user.UserImage),
                new Claim(ClaimTypes.PostalCode, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim(ClaimTypes.GivenName, uid)
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value!));

                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: cred
                    );

                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                    return jwt;
                }
                else
                {
                    throw new BadHttpRequestException("Fill all personal information");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Send(string otp, string recipientEmail)
        {
            try
            {
                string senderEmail = _configuration.GetSection("EmailUserName").Value!; // Replace with your Gmail address
                string senderPassword = _configuration.GetSection("EmailPassword").Value!; // Replace with your Gmail password

                string subject = "OTP Verification";
                string body = $"Your OTP is: {otp}";

                using (SmtpClient smtpClient = new SmtpClient(_configuration.GetSection("EmailHost").Value, 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body))
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        smtpClient.Dispose();
                    };

                    Debug.WriteLine("OTP email sent successfully!");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static bool Validate(
        DateTime? notBefore,
        DateTime? expires,
        SecurityToken tokenToValidate,
        TokenValidationParameters @param
    )
        {
            return (expires != null && expires > DateTime.UtcNow);
        }
    }
}