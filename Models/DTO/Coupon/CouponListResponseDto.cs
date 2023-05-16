#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponListResponseDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public DateTime CouponEndDate { get; set; }
    }
}