using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceDetailRepository
    {
        Task<List<ServiceDetail>?> View(PageDto page);
        Task<List<ServiceDetail>?> FilterService(int serviceId);
        Task<ServiceDetail?> Detail(int id);
        Task Create(ServiceDetail garageDetail);
        Task Update(ServiceDetail garageDetail);
    }
}