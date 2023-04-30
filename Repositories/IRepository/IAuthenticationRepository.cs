using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IAuthenticationRepository{
        Task<UserLoginDto?> CheckLogin(LoginDto login);
        RefreshTokenDto? RefreshToken();
    }
}