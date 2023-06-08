using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> View(PageDto page);
        Task<Category?> Detail(int id);
        Task Create(Category category);
        Task Update(Category category);
    }
}
