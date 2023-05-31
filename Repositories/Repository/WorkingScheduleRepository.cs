using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class WorkingScheduleRepository : IWorkingScheduleRepository
    {
        public readonly DataContext context;
        public WorkingScheduleRepository(DataContext context){
            this.context = context;
        }

        public async Task<List<WorkingSchedule>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<WorkingSchedule>
                .Get(context.WorkingSchedules, page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<WorkingSchedule>?> FilterWorkingScheduleByGarage(int garageId)
        {
            try
            {
                var list = await context.WorkingSchedules
                .Where(w => w.GarageId == garageId)
                .Include(w => w.Mechanic)
                .ThenInclude(m => m.User)
                .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<WorkingSchedule>?> FilterWorkingScheduleByMechanic(int mechanicId)
        {
            try
            {
                var list = await context.WorkingSchedules
                .Where(w => w.MechanicId == mechanicId)
                .Include(w => w.Garage)
                .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<WorkingSchedule>?> FilterWorkingScheduleWhoAvailable(int mechanicId)
        {
            try
            {
                var list = await context.WorkingSchedules
                .Where(w => w.MechanicId == mechanicId
                && w.WorkingScheduleStatus == WorkingScheduleStatus.Available)
                .Include(w => w.Mechanic)
                .ThenInclude(m => m.User)
                .ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WorkingSchedule?> Detail(int id)
        {
            try
            {
                var workingSchedule = await context.WorkingSchedules
                .Where(w => w.WorkingScheduleId == id)
                .Include(w => w.Garage)
                .Include(w => w.Mechanic)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync();
                return workingSchedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(WorkingSchedule workingSchedule)
        {
            try
            {
                context.WorkingSchedules.Add(workingSchedule);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(WorkingSchedule workingSchedule)
        {
            try
            {
                context.WorkingSchedules.Update(workingSchedule);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}