using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>?> View(PageDto page);
        Task<CategoryDto?> Detail(int id);
        Task Create(CreateCategoryDto categoryDto);
        Task Update(UpdateCategoryDto categoryDto);
        Task Delete(DeleteCategoryDto categoryDto);
    }
}
