using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.ServiceGarage;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceService
    {
        Task<List<ServiceListResponseDto>?> View(PageDto page);
        Task<List<ServiceGarageListResponseDto>?> FilterServiceByGarage(int GarageId);
        Task<ServiceDetailResponseDto?> Detail(int id);
        Task<bool> Create(ServiceCreateRequestDto requestDto);
        Task<bool> Update(ServiceUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(ServiceStatusRequestDto requestDto);

    }
}
