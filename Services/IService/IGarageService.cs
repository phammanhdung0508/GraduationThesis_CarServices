using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IGarageService
    {
       Task<List<GarageListResponseDto>?> View(PageDto page);
       Task<GarageDetailResponseDto?> Detail(int id);
       Task<bool> Create(GarageCreateRequestDto requestDto);
       Task<bool> Update(GarageUpdateRequestDto requestDto);
       Task<bool> UpdateStatus(GarageStatusRequestDto requestDto);
       Task<bool> UpdateLocation(LocationUpdateRequestDto requestDto);
       Task<List<GarageListResponseDto>?> FilterGaragesNearMe(LocationRequestDto requestDto);
    }
}