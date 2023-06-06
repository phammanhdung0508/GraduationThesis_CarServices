using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IGarageDetailRepository
    {
        Task<List<GarageDetail>?> FilterServiceByGarage(int garageId);
        Task<GarageDetail?> Detail(int id);
    }
}