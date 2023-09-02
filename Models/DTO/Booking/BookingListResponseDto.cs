namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListResponseDto
    {
        public int BookingId { get; set; }
        public string BookingCode {get; set;} = string.Empty;
        public string BookingTime { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public GarageBookingDto? GarageBookingDto { get; set; }
        public UserBookingDto? UserBookingDto { get; set; }
    }
}