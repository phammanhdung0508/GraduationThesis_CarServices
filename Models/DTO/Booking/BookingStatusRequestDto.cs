#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingStatusRequestDto
    {
        public int BookingId { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}