using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public SubcategoryRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<SubcategoryDto>?> View(PageDto page)
        {
            try
            {
                List<Subcategory> list = await PagingConfiguration<Subcategory>.Get(context.Subcategories, page);
                return mapper.Map<List<SubcategoryDto>>(list);
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
                SubcategoryDto subcategory = mapper.Map<SubcategoryDto>(await context.Subcategories.FirstOrDefaultAsync(c => c.SubcategoryId == id));
                return subcategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateSubcategoryDto subcategoryDto)
        {
            try
            {
                Subcategory subcategory = mapper.Map<Subcategory>(subcategoryDto);
                context.Subcategories.Add(subcategory);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateSubcategoryDto subcategoryDto)
        {
            try
            {
                var subcategory = context.Subcategories.FirstOrDefault(c => c.SubcategoryId == subcategoryDto.SubcategoryId)!;
                mapper.Map<UpdateSubcategoryDto, Subcategory?>(subcategoryDto, subcategory);
                context.Subcategories.Update(subcategory);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteSubcategoryDto subcategoryDto)
        {
            try
            {
                var subcategory = context.Subcategories.FirstOrDefault(c => c.SubcategoryId == subcategoryDto.SubcategoryId)!;
                mapper.Map<DeleteSubcategoryDto, Subcategory?>(subcategoryDto, subcategory);
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