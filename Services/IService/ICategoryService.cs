using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>?> View(PageDto page);
        Task<CategoryDto?> Detail(int id);
        Task<bool> Create(CreateCategoryDto createCategoryDto);
        Task<bool> Update(UpdateCategoryDto updateCategoryDto);
        Task<bool> Delete(DeleteCategoryDto deleteCategoryDto);
    }
}
