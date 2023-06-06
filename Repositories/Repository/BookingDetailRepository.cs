using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly DataContext context;
        public BookingDetailRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<BookingDetail>> FilterServiceBookingByBookingId(int bookingId)
        {
            try
            {
                var list = await context.BookingDetails.Where(s => s.BookingId == bookingId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(List<BookingDetail> serviceBookings)
        {
            try
            {
                for (int i = 0; i < serviceBookings.Count; i++)
                {
                    context.BookingDetails.Add(serviceBookings[i]);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(List<BookingDetail> serviceBookings)
        {
            try
            {
                for (int i = 0; i < serviceBookings.Count; i++)
                {
                    context.BookingDetails.Update(serviceBookings[i]);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}