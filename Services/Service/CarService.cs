using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<CarListResponseDto>?> FilterUserCar(int customerId)
        {
            try
            {
                var isCustomerExist = await userRepository.IsCustomerExist(customerId);

                switch (false)
                {
                    case var isExist when isExist == isCustomerExist:
                        throw new MyException("The customer doesn't exist.", 404);
                }

                var list = mapper
                .Map<List<CarListResponseDto>>(await carRepository.FilterUserCar(customerId));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<CarDetailResponseDto?> Detail(int id)
        {
            try
            {
                var car = mapper
                .Map<CarDetailResponseDto>(await carRepository.Detail(id));

                switch (false)
                {
                    case var isExist when isExist == (car != null):
                        throw new MyException("The car doesn't exist.", 404);
                }

                return car;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<bool> Create(CarCreateRequestDto requestDto)
        {
            try
            {
                //var isCustomerExist = await userRepository.IsCustomerExist(requestDto.CustomerId);
                var isLicensePlateExist = await carRepository.IsLicensePlate(requestDto.CarLicensePlate);

                switch (false)
                {
                    //case var isExist when isExist == isCustomerExist:
                        //throw new MyException("The customer doesn't exist.", 404);
                    case var isExist when isExist != isLicensePlateExist:
                        throw new MyException("The license plate already exists.", 404);
                }

                var car = mapper.Map<CarCreateRequestDto, Car>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.CarStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));
                await carRepository.Create(car);
                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<bool> Update(CarUpdateRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The car doesn't exist.", 404);
                }

                var car = mapper.Map<CarUpdateRequestDto, Car>(requestDto, c!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await carRepository.Update(car);
                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<bool> UpdateStatus(CarStatusRequestDto requestDto)
        {
            try
            {
                var c = await carRepository.Detail(requestDto.CarId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The car doesn't exist.", 404);
                }

                var car = mapper.Map<CarStatusRequestDto, Car>(requestDto, c!);
                await carRepository.Update(car);
                return true;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }
    }
}