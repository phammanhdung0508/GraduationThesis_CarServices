using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICarService
    {
        Task<List<CarDto>?> View(PageDto page);
        Task<CarDto?> Detail(int id);
        Task<bool> Create(CreateCarDto createCarDto);
        Task<bool> Update(UpdateCarDto updateCarDto);
        Task<bool> Delete(DeleteCarDto deleteCarDto);
    }
}