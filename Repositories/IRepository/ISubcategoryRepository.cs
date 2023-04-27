using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ISubcategoryRepository
    {
        Task<List<SubcategoryDto>?> View(PageDto page);
        Task<SubcategoryDto?> Detail(int id);
        Task Create(CreateSubcategoryDto subcategoryDto);
        Task Update(UpdateSubcategoryDto subcategoryDto);
        Task Delete(DeleteSubcategoryDto subcategoryDto);
    }
}