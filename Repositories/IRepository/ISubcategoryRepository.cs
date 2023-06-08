using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> View(PageDto page);
        Task<Subcategory?> Detail(int id);
        Task Create(Subcategory subcategory);
        Task Update(Subcategory subcategory);
    }
}