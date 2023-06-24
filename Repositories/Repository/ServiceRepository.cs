using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Paging;
using Microsoft.EntityFrameworkCore;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext context;
        public ServiceRepository(DataContext context)
        {
            this.context = context;
        }


        public async Task<List<Service>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Service>.Get(context.Services
                .Where(s => s.ServiceStatus == Status.Activate), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CountServiceData()
        {
            try
            {
                var count = await context.Services.CountAsync();
                
                return count;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsServiceExist(int serviceId)
        {
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
                .Include(s => s.GarageDetails)
                .ThenInclude(g => g.Garage)
                .Include(s => s.ServiceDetails)
                .FirstOrDefaultAsync();
                return service;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsDuplicatedService(Service service)
        {
            try
            {
                var check = await context.Services
                .Where(s => s.ServiceName.Equals(service.ServiceName)
                && s.ServiceDetailDescription.Equals(service.ServiceDetailDescription)
                && s.ServiceDuration == service.ServiceDuration
                && s.ServiceStatus == Status.Activate).AnyAsync();

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

        public double GetPrice(int serviceDetailId)
        {
            try
            {
                var servicePrice = context.ServiceDetails
                .Where(p => p.ServiceDetailId.Equals(serviceDetailId))
                .Select(p => p.ServicePrice).FirstOrDefault();

                return servicePrice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetDuration(int serviceDetailId)
        {
            try
            {
                var serviceDuration = await context.ServiceDetails
                .Include(p => p.Service)
                .Where(p => p.ServiceDetailId.Equals(serviceDetailId))
                .Select(p => p.Service.ServiceDuration)
                .FirstOrDefaultAsync();

                return serviceDuration;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
