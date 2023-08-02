using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.Jwt;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IAuthenticationRepository{
        Task<UserLoginDto?> CheckLogin(LoginDto login);
        Task<RefreshTokenDto?> RefreshToken(int userId);
        Task<JWTDto> AuthenFirebase(string idToken);
        Task SendOTP(string recipientPhone);
        Task ValidateOTP(string otp, string recipientPhone);
        Task UserRegister(UserRegisterRequestDto requestDto);
        Task<int> Count(string entity);
        Task ChangePassword(ChangePasswordDto requestDto);
        Task<string> IsEmailExist(string entityName);
        string RecreateToken(UserLoginDto user);
        Task<UserLoginDto> GetUser(int userId);
    }
}