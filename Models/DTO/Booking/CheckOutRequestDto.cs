#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class CheckOutRequestDto
    {
        public List<int> ServiceList { get; set; }
        public int? CouponId { get; set; }
    }
}