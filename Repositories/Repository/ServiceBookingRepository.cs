using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly DataContext context;
        public ServiceBookingRepository(DataContext context)
        {
            this.context = context;
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
    }
}