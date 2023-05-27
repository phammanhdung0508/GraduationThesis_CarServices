using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class MechanicRepository : IMechanicRepository
    {
        private readonly DataContext context;
        public MechanicRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Mechanic>> FilterMechanicsByGarageId(int garageId)
        {
            try
            {
                var list = await context.Mechanics
                .Join(context.WorkingSchedules.Where(w => w.GarageId == garageId), m => m.MechanicId, w => w.MechanicId,
                (m, w) => new { Mechanic = m, WorkingSchedule = w }).Select(m => m.Mechanic).Include(m => m.User).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Mechanic?> Detail(int mechanicId)
        {
            try
            {
                var mechanic = await context.Mechanics
                .Where(m => m.MechanicId == mechanicId).Include(m => m.User)
                .ThenInclude(u => u.Role).FirstOrDefaultAsync();

                return mechanic;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsMechanicExist(int mechanicId)
        {
            try
            {
                var isExist = await context.Mechanics
                .Where(m => m.MechanicId == mechanicId).AnyAsync();

                return isExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<WorkingSchedule>> FilterWorkingSchedulesByMechanicId(int mechanicId)
        {
            try
            {
                var list = await context.WorkingSchedules
                .Where(w => w.MechanicId == mechanicId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Mechanic>> FilterMechanicAvailableByGarageId(int garageId)
        {
            try
            {
                var list =  await context.Mechanics
                .Join(context.WorkingSchedules.Where(w => w.GarageId == garageId && w.WorkingScheduleStatus == WorkingScheduleStatus.Available), 
                m => m.MechanicId, w => w.MechanicId, (m, w) => new { Mechanic = m, WorkingSchedule = w }).Select(m => m.Mechanic)
                .OrderBy(m => m.TotalWorkingHours).ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task Create(Mechanic mechanic)
        {
            try
            {
                context.Mechanics.Add(mechanic);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Mechanic mechanic)
        {
            try
            {
                context.Mechanics.Update(mechanic);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}