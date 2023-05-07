using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext context;
        public CarRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Car>?> FilterUserCar(int userId)
        {
            try
            {
                var list = await context.Cars
                .Where(c => c.UserId == userId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Car?> Detail(int id)
        {
            try
            {
                var car = await context.Cars
                .FirstOrDefaultAsync(c => c.CarId == id);

                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Car car)
        {
            try
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Car car)
        {
            try
            {
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