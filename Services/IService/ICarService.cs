using GraduationThesis_CarServices.Models.DTO.Car;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICarService
    {
        Task<List<CarListResponseDto>?> FilterUserCar(int userId);
        Task<CarDetailResponseDto?> Detail(int id);
        Task Create(CarCreateRequestDto requestDto, int userId);
        Task Update(CarUpdateRequestDto requestDto);
        Task UpdateStatus(CarStatusRequestDto requestDto);
    }
}