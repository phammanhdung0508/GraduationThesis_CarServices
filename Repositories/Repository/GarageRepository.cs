using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class GarageRepository : IGarageRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public GarageRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<GarageDto>?> View(PageDto page)
        {
            try
            {
                List<Garage> list = await PagingConfiguration<Garage>.Get(context.Garages, page);
                return mapper.Map<List<GarageDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GarageDto?> Detail(int id)
        {
            try
            {
                GarageDto garage = mapper.Map<GarageDto>(await context.Garages.FirstOrDefaultAsync(g => g.GarageId == id));
                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateGarageDto garageDto)
        {
            try
            {
                Garage garage = mapper.Map<Garage>(garageDto);
                context.Garages.Add(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateGarageDto garageDto)
        {
            try
            {
                var garage = context.Garages.FirstOrDefault(g => g.GarageId == garageDto.GarageId)!;
                mapper.Map<UpdateGarageDto, Garage?>(garageDto, garage);
                context.Garages.Update(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteGarageDto garageDto)
        {
            try
            {
                var garage = context.Garages.FirstOrDefault(g => g.GarageId == garageDto.GarageId)!;
                mapper.Map<DeleteGarageDto, Garage?>(garageDto, garage);
                context.Garages.Update(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}