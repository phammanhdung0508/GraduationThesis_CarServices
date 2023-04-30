using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceGarageRepository
    {
        Task<ServiceGarage?> Detail(int id);
    }
}