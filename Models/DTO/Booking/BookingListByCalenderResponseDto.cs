namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListByCalenderResponseDto
    {
        public int BookingId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string BookingStart { get; set; } = string.Empty;
        public string BookingEnd { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
    }
}