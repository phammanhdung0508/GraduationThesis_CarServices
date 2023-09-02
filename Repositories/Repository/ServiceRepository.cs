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

        public async Task<(List<Service>, int count)> View(PageDto page)
        {
            try
            {
                var query = context.Services.AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Service>.Get(query, page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Service>> GetAll()
        {
            try
            {
                var list = await context.Services.ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(List<Service>, int count)> FilterServiceByGarage(int garageId, PageDto page)
        {
            try
            {
                var query = context.GarageDetails.Where(d => d.GarageId == garageId)
                .Include(c => c.Service).ThenInclude(s => s.ServiceDetails).Select(s => s.Service).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Service>.Get(query, page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(List<Service>, int count)> SearchByName(string search, PageDto page)
        {
            try
            {
                var searchTrim = search.Trim().Replace(" ", "").ToLower();
                var query = context.Services.Where(s => s.ServiceName.ToLower().Trim().Replace(" ", "").Contains(searchTrim)).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Service>.Get(query, page);

                return (list, count);
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

        public decimal GetPrice(int serviceDetailId)
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

        public async Task<List<Service>> GetServiceByServiceGroup(int garageId)
        {
            try
            {
                var services = await context.GarageDetails
                .Include(g => g.Service).ThenInclude(s => s.ServiceDetails)
                .Where(g => g.GarageId == garageId &&
                g.Service.ServiceStatus.Equals(Status.Activate))
                .Select(g => g.Service).ToListAsync();

                return services;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingDetail>> GetServiceForBookingDetail(int bookingId)
        {
            try
            {
                var list = await context.BookingDetails
                .Include(b => b.ServiceDetail).ThenInclude(s => s.Service).ThenInclude(s => s.Products)
                .Where(b => b.BookingId == bookingId).ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Service>> GetNotSelectedServiceByGarage(int garageId)
        {
            try
            {
                var avaliablelist = await context.GarageDetails
                .Where(g => g.GarageId == garageId)
                .Join(context.Services, g => g.ServiceId, s => s.ServiceId, (g, s) => new { g, s })
                .Where(gs => gs.g.ServiceId == gs.s.ServiceId)
                .Select(gs => gs.s).ToListAsync();

                var Fulllist = await context.Services.ToListAsync();

                var list = Fulllist.Except(avaliablelist).ToList();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Service>> GetALLIdAndNameByGarage()
        {
            try
            {
                var list = await context.Services.ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
