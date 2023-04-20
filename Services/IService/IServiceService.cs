using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceService
    {
        Task<List<ServiceDto>?> View(PageDto page);
        Task<ServiceDto?> Detail(int id);
        Task<bool> Create(CreateServiceDto createServiceDto);
        Task<bool> Update(UpdateServiceDto updateServiceDto);
    }
}
