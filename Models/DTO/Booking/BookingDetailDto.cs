namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailDto
    {
        public int BookingDetailId { get; set; }
        public string ProductCost { get; set; } = string.Empty;
        public string ServiceCost { get; set; } = string.Empty;
        public string BookingDetailStatus { get; set; } = string.Empty;
        public ServiceBookingDetailDto? ServiceBookingDetailDto {get; set;}
        public ProductBookingDetailDto? ProductBookingDetailDto {get; set;}
        public MechanicBookingDetailDto? MechanicBookingDetailDto {get; set;}
    }
}