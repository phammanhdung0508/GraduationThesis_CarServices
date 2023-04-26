using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>?> View(PageDto page)
        {
            try
            {
                List<CategoryDto>? list = await categoryRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDto?> Detail(int id)
        {
            try
            {
                CategoryDto? _category = mapper.Map<CategoryDto>(await categoryRepository.Detail(id));
                return _category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateCategoryDto createCategoryDto)
        {
            try
            {
                await categoryRepository.Create(createCategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                await categoryRepository.Update(updateCategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteCategoryDto deleteCategoryDto)
        {
            try
            {
                await categoryRepository.Delete(deleteCategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
