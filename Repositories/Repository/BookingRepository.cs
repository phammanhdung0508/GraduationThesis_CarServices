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

        public async Task<List<Booking>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Booking>
                .Get(context.Bookings.Include(b => b.Car)
                .Include(b => b.Report).Include(b => b.Garage), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>?> FilterBookingByDate(DateTime dateSelect, int garageId){
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

        public async Task<List<Booking>?> FilterBookingByTimePerDay(DateTime dateTime, int garageId){
            try
            {
                var list = await context.Bookings
                .Where(b => b.BookingTime.Equals(dateTime) && b.GarageId.Equals(garageId))
                .ToListAsync();

                return list;
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
                var booking = await context.Bookings.Include(b => b.Car)
                .Include(b => b.Report).Include(b => b.Garage)
                .FirstOrDefaultAsync(c => c.BookingId == id);
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
    }
}