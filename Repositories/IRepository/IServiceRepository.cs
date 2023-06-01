using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceRepository
    {
        Task<List<Service>?> View(PageDto page);
        Task<bool> IsServiceExist(int serviceId);
        Task<Service?> Detail(int id);
        Task<bool> IsDuplicatedService(Service service);
        Task Create(Service service);
        Task Update(Service service);
        (float price, int duration) GetPriceAndDuration(int serviceId);
    }
}
