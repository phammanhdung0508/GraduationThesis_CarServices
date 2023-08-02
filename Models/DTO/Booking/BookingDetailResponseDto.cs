#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailResponseDto
    {
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public double TotalPrice { get; set; }
        public string BookingStatus { get; set; }
        public GarageBookingDto GarageBookingDto { get; set; }
        // public CarBookingDto CarBookingDto { get; set; }
        public CustomerBookingDto CustomerBookingDto {get; set;}
        public List<BookingDetailDto> BookingDetailDtos {get; set;}
    }
}