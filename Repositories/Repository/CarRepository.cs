using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public CarRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<CarDto>?> View(PageDto page)
        {
            try
            {
                List<Car> list = await PagingConfiguration<Car>.Create(context.Cars, page);
                return mapper.Map<List<CarDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CarDto?> Detail(int id)
        {
            try
            {
                CarDto car = mapper.Map<CarDto>(await context.Cars.FirstOrDefaultAsync(c => c.CarId == id));
                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateCarDto carDto)
        {
            try
            {
                Car car = mapper.Map<Car>(carDto);
                context.Cars.Add(car);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateCarDto carDto)
        {
            try
            {
                var car = context.Cars.FirstOrDefault(c => c.CarId == carDto.CarId)!;
                mapper.Map<UpdateCarDto, Car?>(carDto, car);
                context.Cars.Update(car);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteCarDto carDto)
        {
            try
            {
                var car = context.Cars.FirstOrDefault(c => c.CarId == carDto.CarId)!;
                mapper.Map<DeleteCarDto, Car?>(carDto, car);
                context.Cars.Update(car);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}