using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceRepository
    {
        Task<List<ServiceDto>?> View(PageDto page);
        Task<ServiceDto?> Detail(int id);
        Task Create(CreateServiceDto serviceDto);
        Task Update(UpdateServiceDto serviceDto);
        float GetPrice(int serviceId);
    }
}
