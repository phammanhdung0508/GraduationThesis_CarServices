using AutoMapper;
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

        public async Task<List<ServiceDto>?> View(PageDto page)
        {
            try
            {
                List<ServiceDto>? list = await serviceRepository.View(page);
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<ServiceDto?> Detail(int id)
        {
            try
            {
                ServiceDto? _service = mapper.Map<ServiceDto>(await serviceRepository.Detail(id));
                return _service;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<bool> Create(CreateServiceDto createServiceDto)
        {
            try
            {
                await serviceRepository.Create(createServiceDto);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> Update(UpdateServiceDto updateServiceDto)
        {
            try
            {
                await serviceRepository.Update(updateServiceDto);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        //Temporary don't make delete function because there's no service status
    }
}
