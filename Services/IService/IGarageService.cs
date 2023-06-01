using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IGarageService
    {
       Task<List<GarageListResponseDto>?> View(PageDto page);
       Task<GarageDetailResponseDto?> Detail(int id);
       Task Create(GarageCreateRequestDto requestDto);
       Task Update(GarageUpdateRequestDto requestDto);
       Task UpdateStatus(GarageStatusRequestDto requestDto);
       Task UpdateLocation(LocationUpdateRequestDto requestDto);
       Task<List<GarageListResponseDto>?> FilterGaragesNearMe(LocationRequestDto requestDto);
       Task<List<GarageListResponseDto>?> FilterGaragesWithCoupon(PageDto page);
       Task<List<GarageListResponseDto>?> Search(SearchDto search);
    }
}