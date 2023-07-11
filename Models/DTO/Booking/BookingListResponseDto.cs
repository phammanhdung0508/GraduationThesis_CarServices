#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListResponseDto
    {
        public int BookingId { get; set; }
        public string BookingCode {get; set;}
        public string BookingTime { get; set; }
        public string BookingStatus { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public GarageBookingDto GarageBookingDto { get; set; }
        public UserBookingDto UserBookingDto { get; set; }
    }
}