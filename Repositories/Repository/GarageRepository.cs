using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class GarageRepository : IGarageRepository
    {
        private readonly DataContext context;
        public GarageRepository(DataContext context)
        {
            this.context = context;
        }


        public async Task<List<Garage>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Garage>
                .Get(context.Garages.Include(g => g.User)
                .ThenInclude(u => u.Role), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Garage>?> GetAll()
        {
            try
            {
                var list = await context.Garages
                .Include(g => g.Reviews).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Garage>?> FilterCoupon(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Garage>.Get(context.Garages
                .Where(g => g.Coupons.Count > 0)
                .Include(g => g.Reviews), page);
                
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Garage?> Detail(int id)
        {
            try
            {
                var garage = await context.Garages
                .Where(g => g.GarageId == id)
                .Include(g => g.User).ThenInclude(u => u.Role)
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync();

                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Garage garage)
        {
            try
            {
                context.Garages.Add(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Garage garage)
        {
            try
            {
                context.Garages.Update(garage);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}