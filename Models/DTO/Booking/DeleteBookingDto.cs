#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class DeleteBookingDto
    {
        public int BookingId { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}