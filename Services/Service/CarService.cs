using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;
        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.carRepository = carRepository;
        }

        public async Task<List<CarDto>?> View(PageDto page)
        {
            try
            {
                List<CarDto>? list = await carRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CarDto?> Detail(int id)
        {
            try
            {
                CarDto? car = mapper.Map<CarDto>(await carRepository.Detail(id));
                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateCarDto createCarDto)
        {
            try
            {
                await carRepository.Create(createCarDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateCarDto updateCarDto)
        {
            try
            {
                await carRepository.Update(updateCarDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteCarDto deleteCarDto)
        {
            try
            {
                await carRepository.Delete(deleteCarDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}