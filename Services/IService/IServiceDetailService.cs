using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IServiceDetailService
    {
        Task<List<ServiceDetailListResponseDto>?> View(PageDto page);
        Task<List<ServiceDetailListResponseDto>?> FilterService(int serviceId);
        Task<ServiceDetailDetailResponseDto?> Detail(int id);
        Task<bool> Create(ServiceDetailCreateRequestDto requestDto);
        Task<bool> Update(ServiceDetailUpdateRequestDto requestDto);
        Task<bool> UpdatePrice(ServiceDetailPriceRequestDto requestDto);

    }
}