using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceRepository
    {
        Task<(List<Service>, int count)> View(PageDto page);
        Task<bool> IsServiceExist(int serviceId);
        Task<List<Service>> GetAll();
        Task<Service?> Detail(int id);
        Task<bool> IsDuplicatedService(Service service);
        Task Create(Service service);
        Task Update(Service service);
        decimal GetPrice(int serviceId);
        Task<int> GetDuration(int serviceId);
        Task<(List<Service>, int count)> SearchByName(string search, PageDto page);
        Task<(List<Service>, int count)> FilterServiceByGarage(int garageId, PageDto page);
        Task<List<Service>> GetServiceByServiceGroup(int garageId);
        Task<List<BookingDetail>> GetServiceForBookingDetail(int bookingId);
        Task<List<Service>> GetNotSelectedServiceByGarage(int garageId);
        Task<List<Service>> GetALLIdAndNameByGarage();
    }
}
