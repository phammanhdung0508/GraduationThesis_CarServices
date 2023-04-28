using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ISubcategoryService
    {
        Task<List<SubcategoryDto>?> View(PageDto page);
        Task<SubcategoryDto?> Detail(int id);
        Task<bool> Create(CreateSubcategoryDto createSubcategoryDto);
        Task<bool> Update(UpdateSubcategoryDto updateSubcategoryDto);
        Task<bool> Delete(DeleteSubcategoryDto deleteSubcategoryDto);
    }
}