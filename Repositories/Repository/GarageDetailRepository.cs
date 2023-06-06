using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class GarageDetailRepository : IGarageDetailRepository
    {
        private readonly DataContext context;
        public GarageDetailRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<GarageDetail>?> FilterServiceByGarage(int garageId)
        {
            try
            {
                var list = await context.GarageDetails
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

        public async Task<GarageDetail?> Detail(int id)
        {
            try
            {
                var serviceGarage = await context.GarageDetails.FirstOrDefaultAsync(c => c.GarageId == id);
                return serviceGarage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}