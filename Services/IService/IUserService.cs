using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Paging;

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
        Task<GenericObject<List<CustomerListResponseDto>>> FilterCustomerBookingAtGarage(PageDto page, int garageId);
        Task<GenericObject<List<CustomerListResponseDto>>> FilterCustomer(PageDto page);
        Task<List<UserListResponseDto>?> SearchUser(string search, int roleId);
        Task<GenericObject<List<UserListResponseDto>>> FilterUser(PageDto page, int roleId, int garageId);
        Task CreateMechanic(MechanicCreateRequestDto requestDto, int? garageId);
        Task<GenericObject<List<UserListResponseDto>>> GetStaffByGarage(PagingBookingPerGarageRequestDto requestDto);
        Task<List<GetIdAndNameDto>> GetManagerNotAssignByGarage();
    }
}