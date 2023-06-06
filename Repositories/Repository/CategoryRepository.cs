using GraduationThesis_CarServices.Models;
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
        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Category>.Get(context.Categories, page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category?> Detail(int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Category category)
        {
            try
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Category category)
        {
            try
            {
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
