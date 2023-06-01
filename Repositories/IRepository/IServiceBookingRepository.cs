using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceBookingRepository
    {
        Task<List<ServiceBooking>> FilterServiceBookingByBookingId(int bookingId);
        Task Create(List<ServiceBooking> serviceBooking);
        Task Update(List<ServiceBooking> serviceBookings);
    }
}