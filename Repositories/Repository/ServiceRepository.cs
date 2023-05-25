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


        public async Task<List<Service>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Service>.Get(context.Services, page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsServiceExist(int serviceId){
            try
            {
                var check = await context.Services
                .Where(s => s.ServiceId == serviceId).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Service?> Detail(int id)
        {
            try
            {
                var service = await context.Services
                .Where(s => s.ServiceId == id)
                .Include(s => s.Products)
                .Include(s => s.ServiceGarages)
                .ThenInclude(g => g.Garage)
                .FirstOrDefaultAsync();
                return service;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsDuplicatedService(Service service){
            try
            {
                var check = await context.Services
                .Where(s => s.ServiceName.Equals(service.ServiceName)
                && s.ServiceDetailDescription.Equals(service.ServiceDetailDescription)
                && s.ServicePrice == service.ServicePrice
                && s.ServiceDuration == service.ServiceDuration).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Service service)
        {
            try
            {
                context.Services.Add(service);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Service service)
        {
            try
            {
                context.Services.Update(service);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Temporary don't make delete function because there's no service status
        public (float price, int duration) GetPriceAndDuration(int serviceId)
        {
            try
            {
                var service = context.Services
                .Where(p => p.ServiceId.Equals(serviceId))
                .Select(p => new {
                    p.ServicePrice,
                    p.ServiceDuration
                })
                .FirstOrDefault();

                return (service!.ServicePrice, service.ServiceDuration);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
