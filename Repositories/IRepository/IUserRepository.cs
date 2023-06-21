using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>?> View(PageDto page);
        Task<bool> IsCustomerExist(int customerId);
        Task<int> GetCustomerId(int userId);
        Task<User?> Detail(int id);
        Task<int> Create(User user);
        Task Update(User user);
        Task<List<User>> FilterByRole(PageDto page, int roleId);
        int TotalBooking(int customerId);
        Task<bool> IsEmailExist(string userEmail);
        Task<bool> IsEmailVerifyOtp(string userEmail);
        Task<User?> CustomerDetail(int userId);
        Task<User?> GetUserByEmail(string userEmail);
    }
}