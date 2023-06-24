using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICategoryService
    {
        Task<GenericObject<List<CategoryListResponseDto>>?> View(PageDto page);
        Task<CategoryDetailResponseDto?> Detail(int id);
        Task Create(CategoryCreateRequestDto requestDto);
        Task Update(CategoryUpdateRequestDto requestDto);
        Task UpdateStatus(CategoryStatusRequestDto requestDto);
    }
}
