using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ICarRepository
    {
        Task<List<Car>?> FilterUserCar(int customerId);
        Task<Car?> Detail(int id);
        Task Create(Car car);
        Task Update(Car car);
    }
}