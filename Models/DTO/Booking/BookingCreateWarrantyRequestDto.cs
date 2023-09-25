namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateWarrantyRequestDto
    {
        public int BookingId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string WarrantyDay { get; set; } = string.Empty;
        public string WarrantyTime { get; set; } = string.Empty;
        public List<int>? ServiceList { get; set; }
    }
}