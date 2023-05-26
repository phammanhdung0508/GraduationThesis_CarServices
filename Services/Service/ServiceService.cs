using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.ServiceGarage;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceGarageRepository serviceGarageRepository;

        private readonly IMapper mapper;
        public ServiceService(IServiceRepository serviceRepository, IServiceGarageRepository serviceGarageRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
            this.serviceGarageRepository = serviceGarageRepository;
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

        public async Task<List<ServiceGarageListResponseDto>?> FilterServiceByGarage(int GarageId)
        {
            try
            {
                var list = mapper
                .Map<List<ServiceGarageListResponseDto>>(await serviceGarageRepository.FilterServiceByGarage(GarageId));

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
                switch (await serviceRepository.IsDuplicatedService(service))
                {
                    case false:
                        await serviceRepository.Create(service);
                        return true;
                }
                return false;
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
                switch (await serviceRepository.IsServiceExist(requestDto.ServiceId))
                {
                    case true:
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
                return false;
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
                switch (await serviceRepository.IsServiceExist(requestDto.ServiceId))
                {
                    case true:
                        var s = await serviceRepository.Detail(requestDto.ServiceId);
                        var service = mapper.Map<ServiceStatusRequestDto,
                        GraduationThesis_CarServices.Models.Entity.Service>(requestDto, s!);
                        await serviceRepository.Update(service);
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Temporary don't make delete function because there's no service status
    }
}
