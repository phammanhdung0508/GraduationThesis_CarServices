using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IGarageDetailRepository
    {
        Task<List<GarageDetail>?> View(PageDto page);
        Task<List<GarageDetail>?> FilterServiceByGarage(int garageId);
        Task<GarageDetail?> Detail(int id);
        Task Create(GarageDetail garageDetail);
        Task Update(GarageDetail garageDetail);
    }
}