using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IUserService
    {
        Task<List<UserListResponseDto>?> View(PageDto page);
        Task<UserDetailResponseDto?> Detail(int id);
        Task<bool> Create(UserCreateRequestDto createUserDto);
        Task<bool> Update(UserUpdateRequestDto updateUserDto);
        Task<bool> UpdateRole(UserRoleRequestDto requestDto);
        Task<bool> UpdateStatus(UserStatusRequestDto requestDto);
    }
}