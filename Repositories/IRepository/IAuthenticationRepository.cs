using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.Jwt;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IAuthenticationRepository{
        Task<UserLoginDto?> CheckLogin(LoginDto login);
        RefreshTokenDto? RefreshToken();
        Task<JWTDto> AuthenFirebase(string idToken);
        Task SendOTP(string recipientEmail);
        bool ValidateOTP(string otp);
    }
}