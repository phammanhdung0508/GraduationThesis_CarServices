using CarServices.Models.DTO;

namespace CarServices.Repositories.IRepository{
    public interface IAuthenticationRepository{
        Task<UserDto?> CheckLogin(LoginDto login);
    }
}