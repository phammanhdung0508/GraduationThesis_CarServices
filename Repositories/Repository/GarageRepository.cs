using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;
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
                var list = await PagingConfiguration<Garage>.Get(context.Garages.Include(g => g.Reviews), page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public (int totalServices, int totalOrders) GetServicesAndBookingsPerGarage(int garageId)
        {
            try
            {
                var query = context.Garages.Where(s => s.GarageId == garageId).AsQueryable();

                var totalServices = query.SelectMany(g => g.GarageDetails).Select(g => g.Service).Count();

                var totalOrders = query.SelectMany(g => g.Bookings).Count();

                return (totalServices, totalOrders);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Garage>?> GetAllCoordinates()
        {
            var list = await context.Garages.ToListAsync();

            return list;
        }

        public async Task<bool> IsGarageExist(int garageId)
        {
            try
            {
                var isExist = await context.Garages.Where(g => g.GarageId == garageId).AnyAsync();

                return isExist;
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
                var list = await context.Garages.Include(g => g.Reviews).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Garage>?> Search(SearchDto search)
        {
            try
            {
                var list = await context.Garages.Where(g => g.GarageName.Contains(search.SearchString))
                .Include(g => g.Reviews).ToListAsync();

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
                .Include(g => g.Lots)
                .Include(g => g.Reviews)
                .Include(g => g.Coupons)
                .Include(g => g.User).ThenInclude(u => u.Role)
                .Include(g => g.GarageDetails).ThenInclude(s => s.Service)
                .FirstOrDefaultAsync();

                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Garage?> GetGarage(int id)
        {
            try
            {
                var garage = await context.Garages
                .Where(g => g.GarageId == id)
                .Include(g => g.Lots).FirstOrDefaultAsync();

                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckVersionNumber(int garageId)
        {
            try
            {
                bool check = context.Garages
                .Any(g => g.GarageId == garageId);

                return check;
            }
            catch (System.Exception)
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