using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceService
    {
        Task<GenericObject<List<ServiceListResponseDto>>> View(PageDto page);
        Task<GenericObject<List<ServiceOfServiceDetailDto>>> FilterServiceByGarage(FilterByGarageRequestDto requestDto);
        Task<ServiceDetailResponseDto?> Detail(int id);
        Task Create(ServiceCreateRequestDto requestDto);
        Task Update(ServiceUpdateRequestDto requestDto);
        Task UpdateStatus(int serviceId);
        Task<GenericObject<List<ServiceListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto);
        Task<List<ServiceListMobileResponseDto>> GetAll();
        Task<List<ServiceSelectResponseDto>> GetServiceByServiceGroup(int garageId, int carType);
        Task<List<GetIdAndNameDto>> GetNotSelectedServiceByGarage(int garageId);
        Task<List<GetIdAndNameDto>> GetALLIdAndNameByGarage();
        Task<List<ServiceDetailForWarrantyDto>> GetServiceDetailByBookingId(int bookingId);
    }
}
