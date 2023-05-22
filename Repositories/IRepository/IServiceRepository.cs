using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceRepository
    {
        Task<List<ServiceDto>?> View(PageDto page);
        Task<ServiceDto?> Detail(int id);
        Task Create(CreateServiceDto serviceDto);
        Task Update(UpdateServiceDto serviceDto);
        (float price, int duration) GetPriceAndDuration(int serviceId);
    }
}
