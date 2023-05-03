using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>?> View(PageDto page);
        Task<User?> Detail(int id);
        Task Create(User user);
        Task Update(User user);
    }
}