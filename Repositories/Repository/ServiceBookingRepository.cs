using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly DataContext context;
        public ServiceBookingRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<ServiceBooking>> FilterServiceBookingByBookingId(int bookingId)
        {
            try
            {
                var list = await context.ServiceBookings.Where(s => s.BookingId == bookingId).ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(List<ServiceBooking> serviceBookings)
        {
            try
            {
                for (int i = 0; i < serviceBookings.Count; i++)
                {
                    context.ServiceBookings.Add(serviceBookings[i]);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(List<ServiceBooking> serviceBookings)
        {
            try
            {
                for (int i = 0; i < serviceBookings.Count; i++)
                {
                    context.ServiceBookings.Update(serviceBookings[i]);
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