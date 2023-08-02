using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingDetailRepository
    {
        Task<List<BookingDetail>> FilterBookingDetailByBookingId(int bookingId);
        Task Create(List<BookingDetail> serviceBooking);
        Task Update(List<BookingDetail> serviceBookings);
    }
}