using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceDetailRepository : IServiceDetailRepository
    {
        private readonly DataContext context;
        public ServiceDetailRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<ServiceDetail>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<ServiceDetail>.Get(context.ServiceDetails
                .Include(g => g.Service), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ServiceDetail>?> FilterService(int serviceId)
        {
            try
            {
                var list = await context.ServiceDetails
                .Where(s => s.ServiceId == serviceId)
                .Include(s => s.Service)
                .ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceDetail?> Detail(int id)
        {
            try
            {
                var serviceGarage = await context.ServiceDetails
                .Where(g => g.ServiceDetailId == id)
                .Include(g => g.Service)
                .FirstOrDefaultAsync();
                return serviceGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(ServiceDetail serviceDetail)
        {
            try
            {
                context.ServiceDetails.Add(serviceDetail);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(ServiceDetail serviceDetail)
        {
            try
            {
                context.ServiceDetails.Update(serviceDetail);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}