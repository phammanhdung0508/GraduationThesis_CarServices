using AutoMapper;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.Diagnostics;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ServiceDetailService : IServiceDetailService
    {
        private readonly IServiceDetailRepository serviceDetailRepository;

        private readonly IMapper mapper;
        public ServiceDetailService(IServiceDetailRepository serviceDetailRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceDetailRepository = serviceDetailRepository;
        }

        public async Task<List<ServiceDetailListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<ServiceDetailListResponseDto>>(await serviceDetailRepository.View(page));

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

        public async Task<List<ServiceDetailListResponseDto>?> FilterService(int serviceId)
        {
            try
            {
                var list = mapper
                .Map<List<ServiceDetailListResponseDto>>(await serviceDetailRepository.FilterService(serviceId));

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

        public async Task<ServiceDetailDetailResponseDto?> Detail(int id)
        {
            try
            {
                var service = mapper.Map<ServiceDetailDetailResponseDto>(await serviceDetailRepository.Detail(id));
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

        public async Task Create(ServiceDetailCreateRequestDto requestDto)
        {
            try
            {
                var serviceDetail = mapper.Map<ServiceDetailCreateRequestDto, ServiceDetail>(requestDto);
                await serviceDetailRepository.Create(serviceDetail);
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

        public async Task Update(ServiceDetailUpdateRequestDto requestDto)
        {
            try
            {
                var s = await serviceDetailRepository.Detail(requestDto.ServiceDetailId);

                switch (false)
                {
                    case var isFalse when isFalse == s is not null:
                        throw new MyException("Chi tiết dịch vụ không tồn tại.", 404);
                    case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.ServicePrice):
                        throw new MyException("Chi tiết dịch vụ không để rỗng.", 404);
                    case var isFalse when isFalse == (requestDto.MaxNumberOfCarLot > requestDto.MinNumberOfCarLot):
                        throw new MyException("Số ghế lớn nhất không được nhỏ hơn só ghế nhỏ nhất.", 404);
                }

                var serviceDetail = mapper.Map<ServiceDetailUpdateRequestDto, ServiceDetail>(requestDto, s!);
                await serviceDetailRepository.Update(serviceDetail);
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
