using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
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

        public async Task<(List<Mechanic>, int, List<int>)> View(PageDto page)
        {
            try
            {
                var query = context.Mechanics.Include(u => u.User)
                .Where(u => u.User.RoleId == 3).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Mechanic>.Get(query, page);

                var totalBookingList = new List<int>();

                foreach (var item in list)
                {
                    var totalBooking = await query.Where(s => s.MechanicId == item.MechanicId)
                    .SelectMany(s => s.BookingMechanics).GroupBy(s => s.BookingId).CountAsync();

                    totalBookingList.Add(totalBooking);
                }

                return (list, count, totalBookingList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Mechanic>> FilterMechanicsByGarage(int garageId)
        {
            try
            {
                var list = await context.Mechanics
                .Include(m => m.User).Include(m => m.BookingMechanics).Include(m => m.GarageMechanics)
                .Where(w => w.Level.Equals(MechanicLevel.Level3.ToString()) &&
                w.MechanicStatus == MechanicStatus.Available &&
                w.GarageMechanics.Any(g => g.GarageId == garageId)).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Mechanic>> FilterMechanicsAvailableByGarage(int garageId)
        {
            try
            {
                var list = await context.GarageMechanics
                .Where(w => w.GarageId == garageId &&
                w.Mechanic.MechanicStatus == MechanicStatus.Available &&
                !w.Mechanic.Level.Equals(MechanicLevel.Level3.ToString()))
                .Include(g => g.Mechanic).ThenInclude(g => g.BookingMechanics).Select(b => b.Mechanic).Distinct().ToListAsync();

                return list;
            }
            catch (System.Exception)
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

        // public async Task<List<WorkingSchedule>> FilterWorkingSchedulesByMechanicId(int mechanicId)
        // {
        //     try
        //     {
        //         var list = await context.WorkingSchedules
        //         .Where(w => w.MechanicId == mechanicId).ToListAsync();

        //         return list;
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }

        public async Task<List<Mechanic>> FilterMechanicAvailableByGarageId(int garageId)
        {
            try
            {
                var list = await context.Mechanics
                .Join(context.GarageMechanics.Where(w => w.GarageId == garageId),
                m => m.MechanicId, w => w.MechanicId, (m, w) => new { Mechanic = m, WorkingSchedule = w }).Select(m => m.Mechanic)
                /*.OrderBy(m => m.TotalBookingApplied*/.ToListAsync();

                return list;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<int> Create(Mechanic mechanic)
        {
            try
            {
                context.Mechanics.Add(mechanic);
                await context.SaveChangesAsync();

                return await context.Mechanics.OrderBy(m => m.MechanicId)
                .Select(m => m.MechanicId).LastAsync();
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

        public async Task<List<Mechanic>> GetMechanicByBooking(int bookingId)
        {
            try
            {
                var mechanic = await context.BookingMechanics.Include(b => b.Mechanic).ThenInclude(m => m.User)
                .Where(b => b.BookingId == bookingId &&
                b.BookingMechanicStatus.Equals(Status.Activate)).Select(b => b.Mechanic).ToListAsync();

                return mechanic;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task CreateBookingMechanic(BookingMechanic bookingMechanic)
        {
            try
            {
                context.BookingMechanics.Add(bookingMechanic);
                await context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UpdateBookingMechanic(BookingMechanic bookingMechanic)
        {
            try
            {
                context.BookingMechanics.Update(bookingMechanic);
                await context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<BookingMechanic?> DetailBookingMechanic(int mechanicId, int bookingId)
        {
            try
            {
                var bookingMechanic = await context.BookingMechanics
                .Where(b => b.MechanicId == mechanicId &&
                b.BookingId == bookingId).FirstOrDefaultAsync();

                return bookingMechanic;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<BookingMechanic?> IsCustomerPickMainMechanic(DateTime date)
        {
            try
            {
                var bookingMechanic = await context.BookingMechanics.Where(b => b.WorkingDate == date).FirstOrDefaultAsync();

                return bookingMechanic;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> CountBookingMechanicApplied(int mechanicId)
        {
            try
            {
                var count = await context.BookingMechanics.Where(b => b.MechanicId == mechanicId).CountAsync();

                return count;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}