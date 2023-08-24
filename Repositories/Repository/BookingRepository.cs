using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext context;
        public BookingRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<(List<Booking>?, int count)> View(PageDto page)
        {
            try
            {
                var query = context.Bookings;

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(query.Include(b => b.Car)
                .ThenInclude(c => c.Customer).ThenInclude(c => c.User).Include(b => b.Garage), page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<Booking>?, int count)> FilterBookingByStatus(BookingStatus? status, PageDto page)
        {
            try
            {
                var query = context.Bookings.Where(b => b.BookingStatus == status).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(query.Include(b => b.Car).ThenInclude(c => c.Customer)
                .ThenInclude(c => c.User).Include(b => b.Garage), page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(List<Booking>?, int count)> FilterBookingStatusAndDate(DateTime? dateFrom, DateTime? dateTo, BookingStatus? status, PageDto page)
        {
            try
            {
                IQueryable<Booking>? runQuery = null;
                var mainQuery = context.Bookings.AsQueryable();

                if (status > 0 && status is not null)
                {
                    runQuery = mainQuery.Where(b => b.BookingStatus == status).AsQueryable();
                    if (dateFrom is not null && dateTo is not null)
                    {
                        runQuery = mainQuery.Where(b => b.BookingStatus == status & dateFrom <= b.CreatedAt & b.CreatedAt <= dateTo).AsQueryable();
                    }
                    else
                    {
                        if (dateFrom is not null)
                        {
                            runQuery = mainQuery.Where(b => b.BookingStatus == status & dateFrom <= b.CreatedAt).AsQueryable();
                        }
                        if (dateTo is not null)
                        {
                            runQuery = mainQuery.Where(b => b.BookingStatus == status & b.CreatedAt <= dateTo).AsQueryable();
                        }
                    }
                }
                else
                {
                    if (dateFrom is not null && dateTo is not null)
                    {
                        runQuery = mainQuery.Where(b => dateFrom <= b.CreatedAt & b.CreatedAt <= dateTo).AsQueryable();
                    }
                    if (dateFrom is not null)
                    {
                        runQuery = mainQuery.Where(b => dateFrom <= b.CreatedAt).AsQueryable();
                    }
                    if (dateTo is not null)
                    {
                        runQuery = mainQuery.Where(b => b.CreatedAt <= dateTo).AsQueryable();
                    }
                }

                var count = await runQuery!.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(runQuery!.Include(r => r.Garage).Include(b => b.Car)
                .ThenInclude(c => c.Customer).ThenInclude(c => c.User).Include(b => b.Garage), page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(List<Booking>?, int count)> SearchByBookingCode(int userId, string search, PageDto page)
        {
            try
            {
                var query = context.Users.Where(u => u.UserId == userId).Select(u => u.Customer).SelectMany(c => c.Cars).SelectMany(c => c.Bookings)
                .Where(b => b.BookingCode.Contains(search.ToUpper().Trim())).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(query, page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsBookingExist(int bookingId)
        {
            try
            {
                var isExist = await context.Bookings
                .Where(b => b.BookingId == bookingId).AnyAsync();

                return isExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<Booking>, int count)> FilterBookingByGarage(int garageId, PageDto page)
        {
            try
            {
                var query = context.Bookings.Where(b => b.GarageId == garageId).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(query.Include(b => b.Garage).Include(b => b.Car)
                .ThenInclude(c => c.Customer).ThenInclude(c => c.User), page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>?> FilterBookingByDate(DateTime dateSelect, int garageId)
        {
            try
            {
                var list = await context.Bookings
                .Where(b => b.BookingTime.Date.Equals(dateSelect) && b.GarageId.Equals(garageId))
                .ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> CountBookingByTimePerDay(DateTime dateTime, int garageId)
        {
            try
            {
                var count = await context.Bookings
                .Where(b => b.BookingTime.Equals(dateTime) && b.GarageId.Equals(garageId))
                .CountAsync();

                return count;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(List<Booking>?, int count)> FilterBookingByCustomer(int userId, PageDto page)
        {
            try
            {
                var query = context.Users.Where(u => u.UserId == userId).Select(u => u.Customer).SelectMany(c => c.Cars).SelectMany(c => c.Bookings).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Booking>.Get(query, page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Booking?> Detail(int id)
        {
            try
            {
                var booking = await context.Bookings.Include(b => b.Garage)
                .Include(b => b.Car).ThenInclude(c => c.Customer).ThenInclude(m => m.User)
                .Include(b => b.BookingMechanics).ThenInclude(d => d.Mechanic).ThenInclude(m => m.User)
                .Include(b => b.BookingDetails).ThenInclude(d => d.ServiceDetail).ThenInclude(s => s.Service)
                .Include(b => b.BookingDetails).ThenInclude(d => d.Product).FirstOrDefaultAsync(c => c.BookingId == id);

                return booking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Create(Booking booking)
        {
            try
            {
                context.Bookings.Add(booking);
                await context.SaveChangesAsync();

                return context.Bookings
                .OrderByDescending(b => b.BookingId)
                .Select(b => b.BookingId).First();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Booking booking)
        {
            try
            {
                context.Bookings.Update(booking);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(decimal, decimal, decimal, decimal, decimal, int, int)> CountRevenue(int garageId)
        {
            try
            {
                var query = context.Bookings.Where(b => b.GarageId == garageId &&
                b.BookingStatus.Equals(BookingStatus.Completed) &&
                b.PaymentStatus.Equals(PaymentStatus.Paid)).AsQueryable();

                var amountEarned = await query.SumAsync(b => b.FinalPrice);

                var serviceEarned = await query.SelectMany(b => b.BookingDetails).SumAsync(d => d.ServicePrice);

                var productEarned = await query.SelectMany(b => b.BookingDetails).SumAsync(d => d.ProductPrice);

                var paidQuery = context.Bookings.Where(b => b.GarageId == garageId &&
                b.PaymentStatus.Equals(PaymentStatus.Paid)).AsQueryable();

                var countPaid = await paidQuery.CountAsync();

                var sumPaid = await paidQuery.SumAsync(b => b.FinalPrice);

                var unpaidQuery = context.Bookings.Where(b => b.GarageId == garageId &&
                b.PaymentStatus.Equals(PaymentStatus.Unpaid)).AsQueryable();

                var countUnpaid = await unpaidQuery.CountAsync();

                var sumUnpaid = await unpaidQuery.SumAsync(b => b.FinalPrice);

                return (amountEarned, serviceEarned, productEarned, sumPaid, sumUnpaid, countPaid, countUnpaid);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<(int, int, int)> CountBookingPerStatus(int? garageId)
        {
            try
            {
                var query = context.Bookings.AsQueryable(); ;

                if (garageId is not null)
                {
                    query = context.Bookings.Where(g => g.GarageId == garageId).AsQueryable();
                }

                var pending = await query.Where(b => b.BookingStatus.Equals(BookingStatus.Pending)).CountAsync();

                var canceled = await query.Where(b => b.BookingStatus.Equals(BookingStatus.Canceled)).CountAsync();

                var completed = await query.Where(b => b.BookingStatus.Equals(BookingStatus.Completed)).CountAsync();

                return (pending, canceled, completed);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<List<Booking>> FilterBookingByStatusCustomer(int bookingStatus, int userId)
        {
            var list = await context.Bookings.Include(b => b.Car).ThenInclude(c => c.Customer)
            .ThenInclude(c => c.User).Include(b => b.Garage)
            .Where(b => (int)b.BookingStatus == bookingStatus &&
            b.Car.Customer.User.UserId == userId &&
            b.IsAccepted == true)
            .ToListAsync();

            return list;
        }

        public async Task<Booking> DetailBookingForCustomer(int bookingId)
        {
            try
            {
                var booking = await context.Bookings.Where(b => b.BookingId == bookingId).Include(b => b.BookingDetails)
                .Include(b => b.Car).ThenInclude(c => c.Customer).ThenInclude(c => c.User).Include(b => b.Garage)
                .FirstOrDefaultAsync();

                return booking!;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> FilterBookingByGarage(int garageId)
        {
            try
            {
                var list = await context.Garages.Where(g => g.GarageId == garageId)
                .SelectMany(g => g.Bookings).ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> FilterListBookingByGarageAndDate(int garageId, DateTime date)
        {
            try
            {
                var list = await context.Bookings.Include(b => b.Garage).Include(b => b.Car)
                .Where(b => b.Garage.GarageId == garageId &&
                b.BookingTime.Date.Equals(date) &&
                (b.BookingStatus.Equals(BookingStatus.Pending) ||
                b.BookingStatus.Equals(BookingStatus.CheckIn) ||
                b.BookingStatus.Equals(BookingStatus.Processing) ||
                b.BookingStatus.Equals(BookingStatus.Completed))).ToListAsync();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task ConfirmBookingArePaid(int bookingId)
        {
            try
            {
                await context.Bookings.Where(b => b.BookingId == bookingId)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.PaymentStatus, PaymentStatus.Paid));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}