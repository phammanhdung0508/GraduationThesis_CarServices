using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

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

        public async Task<List<ServiceListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<ServiceListResponseDto>>(await serviceRepository.View(page));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceDetailResponseDto?> Detail(int id)
        {
            try
            {
                var service = mapper.Map<ServiceDetailResponseDto>(await serviceRepository.Detail(id));
                return service;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(ServiceCreateRequestDto requestDto)
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
                await serviceRepository.Create(service);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ServiceUpdateRequestDto requestDto)
        {
            try
            {
                var s = await serviceRepository.Detail(requestDto.ServiceId);
                var service = mapper.Map<ServiceUpdateRequestDto,
                GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await serviceRepository.Update(service);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(ServiceStatusRequestDto requestDto)
        {
            try
            {
                var s = await serviceRepository.Detail(requestDto.ServiceId);
                var service = mapper.Map<ServiceStatusRequestDto,
                GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!);
                await serviceRepository.Update(service);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Temporary don't make delete function because there's no service status
    }
}
