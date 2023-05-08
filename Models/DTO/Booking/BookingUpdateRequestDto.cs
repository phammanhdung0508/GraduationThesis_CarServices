#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingUpdateRequestDto
    {
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}