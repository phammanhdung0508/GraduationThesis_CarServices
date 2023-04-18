using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface IGarageRepository{
        Task<List<GarageDto>?> View(PageDto page);
        Task<GarageDto?> Detail(int id);
        Task Create(CreateGarageDto couponDto);
        Task Update(UpdateGarageDto couponDto);
        Task Delete(DeleteGarageDto couponDto);
    }
}