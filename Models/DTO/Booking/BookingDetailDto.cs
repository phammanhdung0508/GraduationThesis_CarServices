#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailDto
    {
        public int BookingDetailId { get; set; }
        public float ProductCost { get; set; }
        public float ServiceCost { get; set; }
        public ServiceBookingDetailDto ServiceBookingDetailDto {get; set;}
        public ProductBookingDetailDto ProductBookingDetailDto {get; set;}
        public MechanicBookingDetailDto MechanicBookingDetailDto {get; set;}
    }
}