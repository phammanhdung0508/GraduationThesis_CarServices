using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.Entity;
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

        public async Task<List<CarListResponseDto>?> FilterUserCar(int userId)
        {
            try
            {
                var list = mapper
                .Map<List<CarListResponseDto>>(await carRepository.FilterUserCar(userId));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CarDetailResponseDto?> Detail(int id)
        {
            try
            {
                var car = mapper
                .Map<CarDetailResponseDto>(await carRepository.Detail(id));

                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CarCreateRequestDto requestDto)
        {
            try
            {
                var car = mapper.Map<CarCreateRequestDto, Car>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.CarStatus = 1;
                    des.CreatedAt = DateTime.Now;
                }));
                await carRepository.Create(car);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(CarUpdateRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);
                var car = mapper.Map<CarUpdateRequestDto, Car>(requestDto, c!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await carRepository.Update(car);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(CarStatusRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);
                var car = mapper.Map<CarStatusRequestDto, Car>(requestDto, c!);
                await carRepository.Update(car);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}