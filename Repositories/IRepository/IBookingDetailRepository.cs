using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingDetailRepository
    {
        Task<List<BookingDetail>> FilterServiceBookingByBookingId(int bookingId);
        Task Create(List<BookingDetail> serviceBooking);
        Task Update(List<BookingDetail> serviceBookings);
    }
}