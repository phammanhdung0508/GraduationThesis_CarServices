namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class FilterByCustomerResponseDto
    {
        public int BookingId { get; set; }
        public string BookingCode {get; set;} = string.Empty;
        public string BookingTime { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public int GarageId { get; set; }
    }
}