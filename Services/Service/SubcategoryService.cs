using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository subcategoryRepository;
        private readonly IMapper mapper;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.subcategoryRepository = subcategoryRepository;
        }

        public async Task<List<SubcategoryDto>?> View(PageDto page)
        {
            try
            {
                List<SubcategoryDto>? list = await subcategoryRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SubcategoryDto?> Detail(int id)
        {
            try
            {
                SubcategoryDto? subcategory = mapper.Map<SubcategoryDto>(await subcategoryRepository.Detail(id));
                return subcategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateSubcategoryDto createSubcategoryDto)
        {
            try
            {
                await subcategoryRepository.Create(createSubcategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateSubcategoryDto updateSubcategoryDto)
        {
            try
            {
                await subcategoryRepository.Update(updateSubcategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteSubcategoryDto deleteSubcategoryDto)
        {
            try
            {
                await subcategoryRepository.Delete(deleteSubcategoryDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}