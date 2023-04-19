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
                List<Garage> list = await PagingConfiguration<Garage>.Create(context.Garages, page);
                return mapper.Map<List<GarageDto>>(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<GarageDto?> Detail(int id)
        {
            try
            {
                GarageDto garage = mapper.Map<GarageDto>(await context.Garages.FirstOrDefaultAsync(g => g.garage_id == id));
                return garage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task Create(CreateGarageDto couponDto)
        {
            try
            {
                Garage garage = mapper.Map<Garage>(couponDto);
                context.Garages.Add(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task Update(UpdateGarageDto couponDto)
        {
            try
            {
                var garage = context.Garages.FirstOrDefault(g => g.garage_id == couponDto.garage_id)!;
                mapper.Map<UpdateGarageDto, Garage?>(couponDto, garage);
                context.Garages.Update(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task Delete(DeleteGarageDto couponDto)
        {
            try
            {
                var garage = context.Garages.FirstOrDefault(g => g.garage_id == couponDto.garage_id)!;
                mapper.Map<DeleteGarageDto, Garage?>(couponDto, garage);
                context.Garages.Update(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}