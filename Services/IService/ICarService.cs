using GraduationThesis_CarServices.Models.DTO.Car;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICarService
    {
        Task<List<CarListResponseDto>?> FilterUserCar(int userId);
        Task<CarDetailResponseDto?> Detail(int id);
        Task<bool> Create(CarCreateRequestDto requestDto);
        Task<bool> Update(CarUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(CarStatusRequestDto requestDto);
    }
}