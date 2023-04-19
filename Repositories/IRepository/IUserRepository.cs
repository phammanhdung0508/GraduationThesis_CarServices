using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserDto>?> View(PageDto page);
        Task<UserDto?> Detail(int id);
        Task Create(CreateUserDto userDto);
        Task Update(UpdateUserDto userDto);
        Task Delete(DeleteUserDto userDto);
    }
}