using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IUserService
    {
        Task<List<UserListResponseDto>?> View(PageDto page);
        Task<List<UserListResponseDto>?> FilterByRole(PageDto page, int roleId);
        Task<UserDetailResponseDto?> Detail(int id);
        Task Create(UserCreateRequestDto createUserDto);
        Task CustomerFirstLoginUpdate(UserUpdateRequestDto updateUserDto, int userId);
        Task UpdateRole(UserRoleRequestDto requestDto);
        Task UpdateStatus(UserStatusRequestDto requestDto);
        Task<CustomerDetailResponseDto> CustomerDetail(int userId);
    }
}