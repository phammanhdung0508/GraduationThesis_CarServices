#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class FilterByBookingStatusResponseDto
    {
        public int BookingId { get; set; }
        public string BookingTime { get; set; }
        public string CarName {get; set;}
        public string GarageAddress {get; set;}
        public string Price { get; set; }
    }
}