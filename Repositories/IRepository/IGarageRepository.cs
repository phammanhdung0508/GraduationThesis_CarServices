using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IGarageRepository{
        Task<List<Garage>?> View(PageDto page);
        Task<List<Garage>?> GetAll();
        Task<List<Garage>?> FilterCoupon(PageDto page);
        Task<Garage?> Detail(int id);
        Task Create(Garage garage);
        Task Update(Garage garage);
    }
}