using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Paging;
using Microsoft.EntityFrameworkCore;
using GraduationThesis_CarServices.Repositories.IRepository;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ServiceRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<ServiceDto>?> View(PageDto page)
        {
            try
            {
                List<Service> list = await PagingConfiguration<Service>.Get(context.Services, page);
                return mapper.Map<List<ServiceDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceDto?> Detail(int id)
        {
            try
            {
                ServiceDto service = mapper.Map<ServiceDto>(await context.Services.FirstOrDefaultAsync(c => c.ServiceId == id));
                return service;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateServiceDto serviceDto)
        {
            try
            {
                Service service = mapper.Map<Service>(serviceDto);
                context.Services.Add(service);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateServiceDto serviceDto)
        {
            try
            {
                var service = context.Services.FirstOrDefault(c => c.ServiceId == serviceDto.ServiceId)!;
                mapper.Map<UpdateServiceDto, Service?>(serviceDto, service);
                context.Services.Update(service);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Temporary don't make delete function because there's no service status
        public float GetPrice(int serviceId)
        {
            try
            {
                var price = context.Services
                .Where(p => p.ServiceId.Equals(serviceId))
                .Select(p => p.ServicePrice).FirstOrDefault();

                return price;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
