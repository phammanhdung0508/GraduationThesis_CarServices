using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IGarageRepository
    {
        Task<List<Garage>?> View(PageDto page);
        Task<bool> IsGarageExist(int garageId);
        Task<List<Garage>?> GetGrageFilterByDateAndService(List<int> serviceList);
        Task<List<Garage>?> Search(SearchDto search);
        Task<Garage?> Detail(int id);
        Task Create(Garage garage);
        Task Update(Garage garage);
        Task<Garage?> GetGarage(int id);
        bool CheckVersionNumber(int garageId);
        Task<List<Garage>?> GetAllCoordinates();
        (int totalServices, int totalOrders) GetServicesAndBookingsPerGarage(int garageId);
        Task<List<Garage>?> GetAll();
        Task<byte[]?> GetGarageVersionNumber(int garageId);
        Task<int?> GetManagerId(int? garageId);
        Task CreateGarageMechanic(GarageMechanic garageMechanic);
    }
}