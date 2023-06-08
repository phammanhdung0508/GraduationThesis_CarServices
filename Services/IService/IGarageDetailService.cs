using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IGarageDetailService
    {
        Task<List<GarageDetailDetailResponseDto>?> View(PageDto page);
        Task<GarageDetailDetailResponseDto?> Detail(int id);
        Task<bool> Create(GarageDetailCreateRequestDto requestDto);
        Task<bool> Update(GarageDetailUpdateRequestDto requestDto);

    }
}