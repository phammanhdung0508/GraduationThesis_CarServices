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
                List<Garage> list = await PagingConfiguration<Garage>
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
                List<Garage> list = await context.Garages.ToListAsync();
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
                Garage? garage = await context.Garages
                .Include(g => g.User).ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(g => g.GarageId == id);
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