using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ISubcategoryService
    {
        Task<List<SubcategoryDto>?> View(PageDto page);
        Task<SubcategoryDto?> Detail(int id);
        Task Create(CreateSubcategoryDto requestDto);
        Task Update(UpdateSubcategoryDto requestDto);
    }
}