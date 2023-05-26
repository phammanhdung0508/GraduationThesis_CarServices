using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceGarageRepository
    {
        Task<List<ServiceGarage>?> FilterServiceByGarage(int id);
        Task<ServiceGarage?> Detail(int id);
    }
}