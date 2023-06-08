using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceService
    {
        Task<List<ServiceListResponseDto>?> View(PageDto page);
        Task<List<GarageDetailListResponseDto>?> FilterServiceByGarage(int garageId);
        Task<ServiceDetailResponseDto?> Detail(int id);
        Task<bool> Create(ServiceCreateRequestDto requestDto);
        Task<bool> Update(ServiceUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(ServiceStatusRequestDto requestDto);

    }
}
