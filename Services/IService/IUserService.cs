using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IUserService
    {
        Task<List<UserListResponseDto>?> View(PageDto page);
        Task<List<UserListResponseDto>?> FilterByRole(int roleId);
        Task<UserDetailResponseDto?> Detail(int id);
        Task UserRegister(UserCreateRequestDto createUserDto);
        Task Update(UserUpdateRequestDto updateUserDto);
        Task UpdateRole(UserRoleRequestDto requestDto);
        Task UpdateStatus(UserStatusRequestDto requestDto);
    }
}