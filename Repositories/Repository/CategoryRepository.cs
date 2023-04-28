using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDto>?> View(PageDto page)
        {
            try
            {
                List<Category> list = await PagingConfiguration<Category>.Get(context.Categories, page);
                return mapper.Map<List<CategoryDto>>(list);
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
                CategoryDto category = mapper.Map<CategoryDto>(await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id));
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateCategoryDto categoryDto)
        {
            try
            {
                Category category = mapper.Map<Category>(categoryDto);
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateCategoryDto categoryDto)
        {
            try
            {
                var category = context.Categories.FirstOrDefault(c => c.CategoryId == categoryDto.CategoryId)!;
                mapper.Map<UpdateCategoryDto, Category?>(categoryDto, category);
                context.Categories.Update(category);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteCategoryDto categoryDto)
        {
            try
            {
                var category = context.Categories.FirstOrDefault(c => c.CategoryId == categoryDto.CategoryId)!;
                mapper.Map<DeleteCategoryDto, Category?>(categoryDto, category);
                context.Categories.Update(category);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
