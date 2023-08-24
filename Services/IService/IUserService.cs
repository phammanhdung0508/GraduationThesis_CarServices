using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IUserService
    {
        Task<List<UserListResponseDto>?> View(PageDto page);
        Task<UserDetailResponseDto?> Detail(int id);
        Task Create(UserCreateRequestDto createUserDto, int? garageId);
        Task<string> CustomerFirstLoginUpdate(UserUpdateRequestDto updateUserDto, int userId);
        Task UpdateStatus(UserStatusRequestDto requestDto);
        Task<CustomerDetailResponseDto> CustomerDetail(int userId);
        Task<List<CustomerListResponseDto>> SearchCustomer(string search);
        Task<List<CustomerListResponseDto>> FilterCustomer(PageDto page);
        Task<List<UserListResponseDto>?> SearchUser(string search, int roleId);
        Task<List<UserListResponseDto>> FilterUser(PageDto page, int roleId);
        Task CreateMechanic(MechanicCreateRequestDto requestDto, int? garageId);
    }
}