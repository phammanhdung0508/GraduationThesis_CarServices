using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IGarageRepository{
        Task<List<GarageDto>?> View(PageDto page);
        Task<List<Garage>> GetGarageNearUser(User user);
        Task<Garage?> Detail(int id);
        Task Create(CreateGarageDto couponDto);
        Task Update(UpdateGarageDto couponDto);
        Task Delete(DeleteGarageDto couponDto);
    }
}