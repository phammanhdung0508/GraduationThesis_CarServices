using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceDetailService
    {
        Task<List<ServiceDetailListResponseDto>?> View(PageDto page);
        Task<List<ServiceDetailListResponseDto>?> FilterService(int serviceId);
        Task<ServiceDetailDetailResponseDto?> Detail(int id);
        Task Create(ServiceDetailCreateRequestDto requestDto);
        Task Update(ServiceDetailUpdateRequestDto requestDto);
        Task UpdatePrice(ServiceDetailPriceRequestDto requestDto);
    }
}