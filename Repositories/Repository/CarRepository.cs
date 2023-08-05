using GraduationThesis_CarServices.Enum;
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

        public async Task<List<Car>?> FilterUserCar(int customerId)
        {
            try
            {
                var list = await context.Cars
                .Where(c => c.CustomerId == customerId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsCarAvalible(int carId)
        {
            try
            {
                var isAvalible = await context.Cars.Where(c => c.CarId == carId && 
                c.CarBookingStatus == CarStatus.Available).AnyAsync();

                return isAvalible;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsCarExist(int carId)
        {
            try
            {
                var isExist = await context.Cars
                .Where(c => c.CarId == carId).AnyAsync();

                return isExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsLicensePlate(string licensePlate)
        {
            try
            {
                var isExist = await context.Cars
                .Where(c => c.CarLicensePlate.Equals(licensePlate)).AnyAsync();

                return isExist;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<string> GetLicensePlate(int carId)
        {
            try
            {
                var licensePlate = await context.Cars
                .Where(c => c.CarId == carId).Select(c => c.CarLicensePlate)
                .FirstOrDefaultAsync();

                return licensePlate!;
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