using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.Diagnostics;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Mapping;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;

        private readonly IMapper mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
        }

        public async Task<GenericObject<List<ServiceListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count) = await serviceRepository.View(page);

                var listDto = mapper.Map<List<ServiceListResponseDto>>(listObj);

                var list = new GenericObject<List<ServiceListResponseDto>>(listDto, count);

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

        public async Task<List<ServiceSelectResponseDto>> GetServiceByServiceGroup(int garageId, int carType)
        {
            try
            {
                var serviceSelectList = new List<ServiceSelectResponseDto>();

                var serviceGroupList = new List<string>{
                    ServiceGroup.PackageCleaningMaintenance.ToString(),
                    ServiceGroup.PackageExterior.ToString(),
                    ServiceGroup.PackageInterior.ToString(),
                };

                var serviceListByGarage = await serviceRepository.GetServiceByServiceGroup(garageId);

                foreach (var item in serviceGroupList)
                {
                    var serviceList = serviceListByGarage.Where(s => s.ServiceGroup.Equals(item) &&
                    s.ServiceDetails.Count >= 2).ToList();

                    if (serviceList.Count > 0)
                    {
                        var serviceDtoList = mapper.Map<List<GraduationThesis_CarServices.Models.Entity.Service>, List<ServicListDto>>(serviceList,
                        otp => otp.AfterMap((src, des) =>
                        {
                            for (int i = 0; i < src.Count; i++)
                            {
                                var serviceDetail = src[i].ServiceDetails.FirstOrDefault(s => s.MinNumberOfCarLot <= carType && s.MaxNumberOfCarLot >= carType)!;
                                des[i].ServiceDetailId = serviceDetail.ServiceDetailId;
                                des[i].ServicePrice = FormatCurrency.FormatNumber(serviceDetail.ServicePrice) + " VND";
                            }
                        }));

                        serviceSelectList.Add(new ServiceSelectResponseDto { ServiceGroup = item, ServicListDtos = serviceDtoList });
                    }
                    else
                    {
                        continue;
                    }
                }

                return serviceSelectList;
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

        public async Task<List<ServiceListMobileResponseDto>> GetAll()
        {
            try
            {
                var list = await serviceRepository.GetAll();

                var listDto = mapper.Map<List<ServiceListMobileResponseDto>>(list);

                return listDto;
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

        public async Task<GenericObject<List<ServiceListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await serviceRepository.SearchByName(requestDto.Search, page);

                var listDto = mapper.Map<List<ServiceListResponseDto>>(listObj);

                var list = new GenericObject<List<ServiceListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<ServiceOfServiceDetailDto>>> FilterServiceByGarage(FilterByGarageRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await serviceRepository.FilterServiceByGarage(requestDto.GarageId, page);

                var listDto = mapper.Map<List<ServiceOfServiceDetailDto>>(listObj);

                var list = new GenericObject<List<ServiceOfServiceDetailDto>>(listDto, count);

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
                        throw;
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
                    switch (requestDto.ServiceGroup)
                    {
                        case 1:
                            des.ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString();
                            break;
                        case 2:
                            des.ServiceGroup = ServiceGroup.PackageExterior.ToString();
                            break;
                        case 3:
                            des.ServiceGroup = ServiceGroup.PackageInterior.ToString();
                            break;
                    }

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
                        throw;
                }
            }
        }

        public async Task Update(ServiceUpdateRequestDto requestDto)
        {
            try
            {
                switch (false)
                {
                    case var isFail when isFail == typeof(ServiceUnit).IsEnumDefined(requestDto.ServiceUnit!):
                        throw new MyException("Dữ liệu truyền vào không đúng.", 404);
                    case var isFail when isFail == (requestDto.ServiceGroup > 1 && requestDto.ServiceGroup <= 3):
                        throw new MyException("Dữ liệu truyền vào không đúng.", 404);
                }

                if (await serviceRepository.IsServiceExist(requestDto.ServiceId))
                {
                    var s = await serviceRepository.Detail(requestDto.ServiceId);

                    var service = mapper.Map<ServiceUpdateRequestDto,
                    GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!);

                    await serviceRepository.Update(service);
                }
                else
                {
                    throw new MyException("The service doesn't exist.", 404);
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
                        throw;
                }
            }
        }

        public async Task UpdateStatus(int serviceId)
        {
            try
            {
                if (await serviceRepository.IsServiceExist(serviceId))
                {
                    var service = await serviceRepository.Detail(serviceId);

                    switch (service!.ServiceStatus)
                    {
                        case Status.Activate:
                            service.ServiceStatus = Status.Deactivate;
                            break;
                        case Status.Deactivate:
                            service.ServiceStatus = Status.Activate;
                            break;
                    }

                    await serviceRepository.Update(service);
                }
                else
                {
                    throw new MyException("The service doesn't exist.", 404);
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
                        throw;
                }
            }
        }

        public async Task<List<GetIdAndNameDto>> GetNotSelectedServiceByGarage(int garageId)
        {
            try
            {
                var listObj = await serviceRepository.GetNotSelectedServiceByGarage(garageId);

                var listDto = mapper.Map<List<GetIdAndNameDto>>(listObj);

                return listDto;
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

        public async Task<List<GetIdAndNameDto>> GetALLIdAndNameByGarage()
        {
            try
            {
                var listObj = await serviceRepository.GetALLIdAndNameByGarage();

                var listDto = mapper.Map<List<GetIdAndNameDto>>(listObj);

                return listDto;
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
