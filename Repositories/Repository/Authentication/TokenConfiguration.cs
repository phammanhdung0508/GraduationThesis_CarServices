using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class TokenConfiguration
    {
        private readonly IConfiguration _configuration;
        public TokenConfiguration(IConfiguration configuration){
            _configuration = configuration;
        }

        public string CreateToken(UserLoginDto user)
        {


            List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.UserFirstName + user.UserLastName),
            new Claim(ClaimTypes.Email, user.UserEmail),
            new Claim(ClaimTypes.Role, user.RoleDto.RoleName)

        };
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

        public RefreshTokenDto GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokenDto
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddHours(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }
    }
}