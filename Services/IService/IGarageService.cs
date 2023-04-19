using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IGarageService
    {
       Task<List<GarageDto>?> View(PageDto page);
       Task<GarageDto?> Detail(int id);
       Task<bool> Create(CreateGarageDto createGarageDto);
       Task<bool> Update(UpdateGarageDto updateGarageDto);
       Task<bool> Delete(DeleteGarageDto deleteGarageDto); 
    }
}