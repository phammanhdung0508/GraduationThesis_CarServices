using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.Diagnostics;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageDetailRepository garageDetailRepository;

        private readonly IMapper mapper;
        public ServiceService(IServiceRepository serviceRepository, IGarageDetailRepository garageDetailRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
            this.garageDetailRepository = garageDetailRepository;
        }

        public async Task<List<ServiceListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<ServiceListResponseDto>>(await serviceRepository.View(page));
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<List<GarageDetailListResponseDto>?> FilterServiceByGarage(int garageId)
        {
            try
            {
                var list = mapper
                .Map<List<GarageDetailListResponseDto>>(await garageDetailRepository.FilterServiceByGarage(garageId));

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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<ServiceDetailResponseDto?> Detail(int id)
        {
            try
            {
                var service = mapper.Map<ServiceDetailResponseDto>(await serviceRepository.Detail(id));
                return service;
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Create(ServiceCreateRequestDto requestDto)
        {
            try
            {
                var service = mapper.Map<ServiceCreateRequestDto,
                GraduationThesis_CarServices.Models.Entity.Service>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ServiceStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));
                if (!await serviceRepository.IsDuplicatedService(service))
                {
                    await serviceRepository.Create(service);
                }
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Update(ServiceUpdateRequestDto requestDto)
        {
            try
            {
                if (await serviceRepository.IsServiceExist(requestDto.ServiceId))
                {
                    var s = await serviceRepository.Detail(requestDto.ServiceId);
                    var service = mapper.Map<ServiceUpdateRequestDto,
                    GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!,
                    otp => otp.AfterMap((src, des) =>
                    {
                        des.UpdatedAt = DateTime.Now;
                    }));
                    await serviceRepository.Update(service);
                }
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdateStatus(ServiceStatusRequestDto requestDto)
        {
            try
            {
                if (await serviceRepository.IsServiceExist(requestDto.ServiceId))
                {
                    var s = await serviceRepository.Detail(requestDto.ServiceId);
                    var service = mapper.Map<ServiceStatusRequestDto,
                    GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!);
                    await serviceRepository.Update(service);
                }
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        //Temporary don't make delete function because there's no service status
    }
}
