namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class ServiceBookingDetailDto
    {
        public int ServiceId { get; set; }
        public int ServiceDuration { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceImage { get; set; } = string.Empty;
        public string ServiceCost { get; set; } = string.Empty;
        public string ServiceWarranty {get; set;} = string.Empty;
    }
}