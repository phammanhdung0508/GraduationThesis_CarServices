using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly DataContext context;
        public SubcategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Subcategory>> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Subcategory>.Get(context.Subcategories, page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Subcategory?> Detail(int id)
        {
            try
            {
                var subcategory = await context.Subcategories.FirstOrDefaultAsync(c => c.SubcategoryId == id);

                return subcategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Subcategory subcategory)
        {
            try
            {
                context.Subcategories.Add(subcategory);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Subcategory subcategory)
        {
            try
            {
                context.Subcategories.Update(subcategory);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}