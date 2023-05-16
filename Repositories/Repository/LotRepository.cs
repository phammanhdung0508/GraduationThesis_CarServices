using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class LotRepository : ILotRepository
    {
        private readonly DataContext context;
        public LotRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Lot>?> GetAllLotInGarage(int garageId)
        {
            try
            {
                var list = await context.Lots
                .Where(l => l.GarageId == garageId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Lot?> IsFree()
        {
            try
            {
                var lot = await context.Lots
                .Where(l => l.LotStatus.Equals(LotStatus.Free)).OrderBy(l => l.LotStatus).FirstOrDefaultAsync();

                return lot;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Lot lot)
        {
            try
            {
                context.Lots.Update(lot);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}