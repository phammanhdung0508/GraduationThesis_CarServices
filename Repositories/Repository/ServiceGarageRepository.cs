using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceGarageRepository : IServiceGarageRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ServiceGarageRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ServiceGarage>?> FilterServiceByGarage(int garageId)
        {
            try
            {
                var list = await context.ServiceGarages
                .Where(s => s.GarageId == garageId)
                .Include(s => s.Service)
                .ThenInclude(s => s.Products)
                .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceGarage?> Detail(int id)
        {
            try
            {
                ServiceGarage? serviceGarage = await context.ServiceGarages.FirstOrDefaultAsync(c => c.GarageId == id);
                return serviceGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}