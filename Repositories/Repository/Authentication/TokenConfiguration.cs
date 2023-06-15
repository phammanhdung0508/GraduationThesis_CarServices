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
                new Claim(ClaimTypes.Name, user.UserFirstName + user.UserLastName),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Role, user.RoleDto.RoleName)};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value!));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: cred
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public RefreshTokenDto GenerateRefreshToken()
        {
            try
            {
                var refreshToken = new RefreshTokenDto
                {
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expires = DateTime.Now.AddHours(7),
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
                new Claim(ClaimTypes.PostalCode, user.UserId + "RandomString"),
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

                // Get the current timestamp
                // DateTime currentTimestamp = DateTime.Now;

                // Set the OTP validity duration (1 minute in this example)
                // TimeSpan validityDuration = TimeSpan.FromMinutes(1);

                // Calculate the OTP expiration timestamp
                // DateTime otpExpiration = currentTimestamp.Add(validityDuration);

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
    }
}