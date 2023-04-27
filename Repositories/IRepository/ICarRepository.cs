using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ICarRepository
    {
        Task<List<CarDto>?> View(PageDto page);
        Task<CarDto?> Detail(int id);
        Task Create(CreateCarDto carDto);
        Task Update(UpdateCarDto carDto);
        Task Delete(DeleteCarDto carDto);
    }
}