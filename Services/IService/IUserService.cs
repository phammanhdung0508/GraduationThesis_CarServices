using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IUserService
    {
        Task<List<UserDto>?> View(PageDto page);
        Task<UserDto?> Detail(int id);
        Task<bool> Create(CreateUserDto createUserDto);
        Task<bool> Update(UpdateUserDto updateUserDto);
        Task<bool> Delete(DeleteUserDto deleteUserDto);
    }
}