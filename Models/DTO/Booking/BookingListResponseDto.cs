#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListResponseDto
    {
        public DateTime BookingTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public GarageBookingDto GarageBookingDto { get; set; }
        public CarBookingDto CarBookingDto { get; set; }
        public ReportBookingDto ReportBookingDto { get; set; }
    }
}